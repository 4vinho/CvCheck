using api.App.Contracts.Auth;
using api.App.Controllers;
using api.Core.Entities;
using api.Core.Interfaces.Auth;
using api.Infra.Services.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace backend.Tests.Auth;

public sealed class LoginLogoutUnitTests
{
    [Fact]
    public async Task LoginService_WithUnknownEmail_ReturnsValidationFailure()
    {
        var userManager = TestUserManagerFactory.Create();
        var signInManager = TestSignInManagerFactory.Create(userManager);
        var service = new LoginService(userManager, signInManager);

        var result = await service.LoginAsync(new LoginRequest
        {
            Email = "missing@example.com",
            Password = "StrongPass1!"
        });

        Assert.False(result.Succeeded);
        Assert.False(result.RequiresEmailConfirmation);
        Assert.True(result.Errors.ContainsKey("email"));
        Assert.False(signInManager.SignInWasCalled);
    }

    [Fact]
    public async Task LoginService_WithInvalidPassword_ReturnsValidationFailure()
    {
        var userManager = TestUserManagerFactory.Create();
        var signInManager = TestSignInManagerFactory.Create(userManager);
        var service = new LoginService(userManager, signInManager);

        await CreateUserAsync(userManager, "valid@example.com", "StrongPass1!", emailConfirmed: true);

        var result = await service.LoginAsync(new LoginRequest
        {
            Email = "valid@example.com",
            Password = "WrongPass1!"
        });

        Assert.False(result.Succeeded);
        Assert.False(result.RequiresEmailConfirmation);
        Assert.True(result.Errors.ContainsKey("email"));
        Assert.False(signInManager.SignInWasCalled);
    }

    [Fact]
    public async Task LoginService_WithUnconfirmedAccount_ReturnsPendingEmailConfirmation()
    {
        var userManager = TestUserManagerFactory.Create();
        var signInManager = TestSignInManagerFactory.Create(userManager);
        var service = new LoginService(userManager, signInManager);

        await CreateUserAsync(userManager, "pending@example.com", "StrongPass1!", emailConfirmed: false);

        var result = await service.LoginAsync(new LoginRequest
        {
            Email = "pending@example.com",
            Password = "StrongPass1!"
        });

        Assert.False(result.Succeeded);
        Assert.True(result.RequiresEmailConfirmation);
        Assert.Equal("pending@example.com", result.Email);
        Assert.False(signInManager.SignInWasCalled);
    }

    [Fact]
    public async Task LoginService_WithConfirmedAccount_SignsInAndReturnsAuthenticated()
    {
        var userManager = TestUserManagerFactory.Create();
        var signInManager = TestSignInManagerFactory.Create(userManager);
        var service = new LoginService(userManager, signInManager);

        await CreateUserAsync(userManager, "confirmed@example.com", "StrongPass1!", emailConfirmed: true);

        var result = await service.LoginAsync(new LoginRequest
        {
            Email = "confirmed@example.com",
            Password = "StrongPass1!"
        });

        Assert.True(result.Succeeded);
        Assert.False(result.RequiresEmailConfirmation);
        Assert.Equal("confirmed@example.com", result.Email);
        Assert.True(signInManager.SignInWasCalled);
    }

    [Fact]
    public async Task LogoutService_CallsSignOutOnSignInManager()
    {
        var userManager = TestUserManagerFactory.Create();
        var signInManager = TestSignInManagerFactory.Create(userManager);
        var service = new LogoutService(signInManager);

        await service.LogoutAsync();

        Assert.True(signInManager.SignOutWasCalled);
    }

    [Fact]
    public async Task LogoutService_WithCancelledToken_ThrowsAndDoesNotSignOut()
    {
        var userManager = TestUserManagerFactory.Create();
        var signInManager = TestSignInManagerFactory.Create(userManager);
        var service = new LogoutService(signInManager);
        using var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(() => service.LogoutAsync(cancellationTokenSource.Token));
        Assert.False(signInManager.SignOutWasCalled);
    }

    [Fact]
    public async Task AuthController_Logout_ReturnsNoContent()
    {
        var logoutService = new SpyLogoutService();
        var controller = new AuthController(
            new StubRegistrationService(),
            new StubLoginService(),
            logoutService,
            new StubEmailConfirmationService());

        var result = await controller.Logout(CancellationToken.None);

        Assert.IsType<NoContentResult>(result);
        Assert.True(logoutService.WasCalled);
    }

    private static async Task CreateUserAsync(
        UserManager<ApplicationUser> userManager,
        string email,
        string password,
        bool emailConfirmed)
    {
        var user = new ApplicationUser
        {
            Email = email,
            UserName = email,
            EmailConfirmed = emailConfirmed
        };

        var result = await userManager.CreateAsync(user, password);
        Assert.True(result.Succeeded);
    }

    private sealed class TestSignInManager : SignInManager<ApplicationUser>
    {
        public TestSignInManager(UserManager<ApplicationUser> userManager)
            : base(
                userManager,
                new HttpContextAccessor { HttpContext = new DefaultHttpContext() },
                new TestClaimsPrincipalFactory(),
                Microsoft.Extensions.Options.Options.Create(new IdentityOptions()),
                new Logger<SignInManager<ApplicationUser>>(new LoggerFactory()),
                new AuthenticationSchemeProvider(Microsoft.Extensions.Options.Options.Create(new AuthenticationOptions())),
                new DefaultUserConfirmation<ApplicationUser>())
        {
        }

        public bool SignInWasCalled { get; private set; }

        public bool SignOutWasCalled { get; private set; }

        public override Task SignInAsync(ApplicationUser user, AuthenticationProperties? authenticationProperties, string? authenticationMethod = null)
        {
            SignInWasCalled = true;
            return Task.CompletedTask;
        }

        public override Task SignOutAsync()
        {
            SignOutWasCalled = true;
            return Task.CompletedTask;
        }
    }

    private static class TestSignInManagerFactory
    {
        public static TestSignInManager Create(UserManager<ApplicationUser> userManager) => new(userManager);
    }

    private sealed class TestClaimsPrincipalFactory : IUserClaimsPrincipalFactory<ApplicationUser>
    {
        public Task<System.Security.Claims.ClaimsPrincipal> CreateAsync(ApplicationUser user) =>
            Task.FromResult(new System.Security.Claims.ClaimsPrincipal());
    }

    private sealed class SpyLogoutService : ILogoutService
    {
        public bool WasCalled { get; private set; }

        public Task LogoutAsync(CancellationToken cancellationToken = default)
        {
            WasCalled = true;
            return Task.CompletedTask;
        }
    }

    private sealed class StubRegistrationService : IRegistrationService
    {
        public Task<api.Core.Results.Auth.RegistrationResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default) =>
            Task.FromResult(api.Core.Results.Auth.RegistrationResult.Success(request.Email));
    }

    private sealed class StubLoginService : ILoginService
    {
        public Task<api.Core.Results.Auth.LoginResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default) =>
            Task.FromResult(api.Core.Results.Auth.LoginResult.Authenticated(request.Email));
    }

    private sealed class StubEmailConfirmationService : IEmailConfirmationService
    {
        public Task<api.Core.Results.Auth.ConfirmEmailResult> ConfirmAsync(string email, string code, CancellationToken cancellationToken = default) =>
            Task.FromResult(api.Core.Results.Auth.ConfirmEmailResult.Success());

        public Task GenerateAndSendCodeAsync(ApplicationUser user, CancellationToken cancellationToken = default) =>
            Task.CompletedTask;

        public Task<api.Core.Results.Auth.ResendEmailConfirmationResult> ResendAsync(string email, CancellationToken cancellationToken = default) =>
            Task.FromResult(api.Core.Results.Auth.ResendEmailConfirmationResult.Success());

        public Task<api.Core.Results.Auth.EmailConfirmationResendAvailabilityResult> GetResendAvailabilityAsync(
            string email,
            CancellationToken cancellationToken = default) =>
            Task.FromResult(api.Core.Results.Auth.EmailConfirmationResendAvailabilityResult.Success(true, 0));
    }
}
