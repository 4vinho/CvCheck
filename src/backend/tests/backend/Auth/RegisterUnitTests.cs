using System.ComponentModel.DataAnnotations;
using api.App.Contracts.Auth;
using api.Core.Entities;
using api.Infra.Auth;
using api.Infra.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Tests.Auth;

public sealed class RegisterUnitTests
{
    [Fact]
    public void RegisterRequest_WithDifferentConfirmPassword_ReturnsValidationError()
    {
        var request = new RegisterRequest
        {
            Email = "valid@example.com",
            Password = "StrongPass1!",
            ConfirmPassword = "StrongPass2!"
        };

        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(
            request,
            new ValidationContext(request),
            validationResults,
            validateAllProperties: true);

        Assert.False(isValid);
        Assert.Contains(validationResults, result => result.MemberNames.Contains(nameof(RegisterRequest.ConfirmPassword)));
    }

    [Fact]
    public void IdentityErrorMapper_MapsInvalidEmailToEmailField()
    {
        var result = IdentityResult.Failed(new IdentityError
        {
            Code = nameof(IdentityErrorDescriber.InvalidEmail),
            Description = "Invalid email."
        });

        var errors = IdentityErrorMapper.ToValidationErrors(result);

        Assert.True(errors.ContainsKey("email"));
    }

    [Fact]
    public void IdentityErrorMapper_MapsWeakPasswordToPasswordField()
    {
        var result = IdentityResult.Failed(new IdentityError
        {
            Code = nameof(IdentityErrorDescriber.PasswordTooShort),
            Description = "Password too short."
        });

        var errors = IdentityErrorMapper.ToValidationErrors(result);

        Assert.True(errors.ContainsKey("password"));
    }

    [Fact]
    public void IdentityErrorMapper_MapsDuplicateEmailToEmailField()
    {
        var result = IdentityResult.Failed(new IdentityError
        {
            Code = nameof(IdentityErrorDescriber.DuplicateEmail),
            Description = "Email already in use."
        });

        var errors = IdentityErrorMapper.ToValidationErrors(result);

        Assert.True(errors.ContainsKey("email"));
    }

    [Fact]
    public async Task RegistrationService_WithValidLocalRegistration_GeneratesConfirmationCodeAndSendsEmail()
    {
        await using var fixture = await AuthServiceFixture.CreateAsync();
        var service = fixture.GetRequiredService<RegistrationService>();

        var request = new RegisterRequest
        {
            Email = "pending@example.com",
            Password = "StrongPass1!",
            ConfirmPassword = "StrongPass1!"
        };

        var result = await service.RegisterAsync(request);
        var user = await fixture.UserManager.FindByEmailAsync(request.Email);
        var codes = await fixture.DbContext.EmailConfirmationCodes.Where(code => code.UserId == user!.Id).ToListAsync();

        Assert.True(result.Succeeded);
        Assert.Equal(request.Email, result.Email);
        Assert.NotNull(user);
        Assert.False(user.EmailConfirmed);
        Assert.Single(codes);
        Assert.Equal("ABC123", codes[0].Code);
        Assert.Single(fixture.EmailDeliveryService.Deliveries);
        Assert.Equal(request.Email, fixture.EmailDeliveryService.Deliveries[0].Email);
        Assert.Equal("ABC123", fixture.EmailDeliveryService.Deliveries[0].Code);
    }

    [Fact]
    public async Task RegistrationService_WhenUserCreationFails_DoesNotGenerateCodeOrSendEmail()
    {
        var userManager = TestUserManagerFactory.Create();
        var emailConfirmationService = new SpyEmailConfirmationService();
        var service = new RegistrationService(userManager, emailConfirmationService);

        var request = new RegisterRequest
        {
            Email = "invalid-email",
            Password = "StrongPass1!",
            ConfirmPassword = "StrongPass1!"
        };

        var result = await service.RegisterAsync(request);

        Assert.False(result.Succeeded);
        Assert.False(emailConfirmationService.GenerateWasCalled);
    }

    [Fact]
    public async Task EmailConfirmationService_Resend_WithPendingAccount_GeneratesNewCodeAndInvalidatesPrevious()
    {
        await using var fixture = await AuthServiceFixture.CreateAsync();
        var emailConfirmationService = fixture.GetRequiredService<EmailConfirmationService>();

        var user = await fixture.CreateUserAsync("resend@example.com", "StrongPass1!", emailConfirmed: false);

        await emailConfirmationService.GenerateAndSendCodeAsync(user);
        fixture.TimeProvider.Advance(TimeSpan.FromMinutes(2));

        var result = await emailConfirmationService.ResendAsync(user.Email!);
        var codes = await fixture.DbContext.EmailConfirmationCodes
            .Where(code => code.UserId == user.Id)
            .OrderBy(code => code.CreatedAtUtc)
            .ToListAsync();

        Assert.True(result.Succeeded);
        Assert.Equal(2, codes.Count);
        Assert.NotNull(codes[0].InvalidatedAtUtc);
        Assert.Null(codes[1].InvalidatedAtUtc);
        Assert.Equal("DEF456", codes[1].Code);
        Assert.Equal(2, fixture.EmailDeliveryService.Deliveries.Count);
    }

    [Fact]
    public async Task EmailConfirmationService_Resend_WithConfirmedAccount_ReturnsValidationError()
    {
        await using var fixture = await AuthServiceFixture.CreateAsync();
        var emailConfirmationService = fixture.GetRequiredService<EmailConfirmationService>();

        await fixture.CreateUserAsync("confirmed@example.com", "StrongPass1!", emailConfirmed: true);
        var result = await emailConfirmationService.ResendAsync("confirmed@example.com");

        Assert.False(result.Succeeded);
        Assert.True(result.Errors.ContainsKey("email"));
    }

    [Fact]
    public async Task EmailConfirmationService_Confirm_WithValidCode_MarksAccountAsConfirmed()
    {
        await using var fixture = await AuthServiceFixture.CreateAsync();
        var emailConfirmationService = fixture.GetRequiredService<EmailConfirmationService>();

        var user = await fixture.CreateUserAsync("confirm@example.com", "StrongPass1!", emailConfirmed: false);
        await emailConfirmationService.GenerateAndSendCodeAsync(user);

        var result = await emailConfirmationService.ConfirmAsync(user.Email!, "ABC123");
        var reloadedUser = await fixture.UserManager.FindByEmailAsync(user.Email!);
        var storedCode = await fixture.DbContext.EmailConfirmationCodes.SingleAsync(code => code.UserId == user.Id);

        Assert.True(result.Succeeded);
        Assert.NotNull(reloadedUser);
        Assert.True(reloadedUser.EmailConfirmed);
        Assert.NotNull(storedCode.ConsumedAtUtc);
    }

    [Fact]
    public async Task EmailConfirmationService_Confirm_WithExpiredCode_DoesNotConfirmAccount()
    {
        await using var fixture = await AuthServiceFixture.CreateAsync();
        var emailConfirmationService = fixture.GetRequiredService<EmailConfirmationService>();

        var user = await fixture.CreateUserAsync("expired@example.com", "StrongPass1!", emailConfirmed: false);
        await emailConfirmationService.GenerateAndSendCodeAsync(user);
        fixture.TimeProvider.Advance(TimeSpan.FromMinutes(16));

        var result = await emailConfirmationService.ConfirmAsync(user.Email!, "ABC123");
        var reloadedUser = await fixture.UserManager.FindByEmailAsync(user.Email!);

        Assert.False(result.Succeeded);
        Assert.False(reloadedUser!.EmailConfirmed);
        Assert.True(result.Errors.ContainsKey("code"));
    }

    [Fact]
    public async Task EmailConfirmationService_Confirm_WithSupersededCode_DoesNotConfirmAccount()
    {
        await using var fixture = await AuthServiceFixture.CreateAsync();
        var emailConfirmationService = fixture.GetRequiredService<EmailConfirmationService>();

        var user = await fixture.CreateUserAsync("superseded@example.com", "StrongPass1!", emailConfirmed: false);
        await emailConfirmationService.GenerateAndSendCodeAsync(user);
        fixture.TimeProvider.Advance(TimeSpan.FromMinutes(2));
        await emailConfirmationService.ResendAsync(user.Email!);

        var result = await emailConfirmationService.ConfirmAsync(user.Email!, "ABC123");
        var reloadedUser = await fixture.UserManager.FindByEmailAsync(user.Email!);

        Assert.False(result.Succeeded);
        Assert.False(reloadedUser!.EmailConfirmed);
        Assert.True(result.Errors.ContainsKey("code"));
    }

    private sealed class SpyEmailConfirmationService : api.Core.Interfaces.Auth.IEmailConfirmationService
    {
        public bool GenerateWasCalled { get; private set; }

        public Task<api.Core.Results.Auth.ConfirmEmailResult> ConfirmAsync(string email, string code, CancellationToken cancellationToken = default) =>
            Task.FromResult(api.Core.Results.Auth.ConfirmEmailResult.Success());

        public Task GenerateAndSendCodeAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            GenerateWasCalled = true;
            return Task.CompletedTask;
        }

        public Task<api.Core.Results.Auth.ResendEmailConfirmationResult> ResendAsync(string email, CancellationToken cancellationToken = default) =>
            Task.FromResult(api.Core.Results.Auth.ResendEmailConfirmationResult.Success());
    }

    private sealed class AuthServiceFixture : IAsyncDisposable
    {
        private readonly ServiceProvider serviceProvider;

        private AuthServiceFixture(ServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            DbContext = serviceProvider.GetRequiredService<api.Infra.Data.ApplicationDbContext>();
            UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            EmailDeliveryService = (TestEmailConfirmationDeliveryService)serviceProvider.GetRequiredService<api.Core.Interfaces.Auth.IEmailConfirmationDeliveryService>();
            TimeProvider = (MutableTimeProvider)serviceProvider.GetRequiredService<api.Core.Interfaces.ITimeProvider>();
        }

        public api.Infra.Data.ApplicationDbContext DbContext { get; }

        public UserManager<ApplicationUser> UserManager { get; }

        public TestEmailConfirmationDeliveryService EmailDeliveryService { get; }

        public MutableTimeProvider TimeProvider { get; }

        public static async Task<AuthServiceFixture> CreateAsync()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<api.Infra.Data.ApplicationDbContext>(options =>
                options.UseInMemoryDatabase($"auth-unit-tests-{Guid.NewGuid():N}"));
            services
                .AddIdentityCore<ApplicationUser>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<api.Infra.Data.ApplicationDbContext>();

            var timeProvider = new MutableTimeProvider(new DateTimeOffset(2026, 4, 26, 12, 0, 0, TimeSpan.Zero));
            services.AddSingleton<api.Core.Interfaces.ITimeProvider>(timeProvider);
            services.AddSingleton<api.Core.Interfaces.Auth.IConfirmationCodeGenerator>(new TestConfirmationCodeGenerator("ABC123", "DEF456", "GHI789"));
            services.AddSingleton<api.Core.Interfaces.Auth.IEmailConfirmationDeliveryService>(new TestEmailConfirmationDeliveryService());
            services.AddScoped<api.Core.Interfaces.Auth.IEmailConfirmationService, EmailConfirmationService>();
            services.AddScoped<RegistrationService>();
            services.AddScoped<EmailConfirmationService>();

            var provider = services.BuildServiceProvider();
            await provider.GetRequiredService<api.Infra.Data.ApplicationDbContext>().Database.EnsureCreatedAsync();
            return new AuthServiceFixture(provider);
        }

        public T GetRequiredService<T>() where T : notnull => serviceProvider.GetRequiredService<T>();

        public async Task<ApplicationUser> CreateUserAsync(string email, string password, bool emailConfirmed)
        {
            var user = new ApplicationUser
            {
                Email = email,
                UserName = email,
                EmailConfirmed = emailConfirmed
            };

            var result = await UserManager.CreateAsync(user, password);
            Assert.True(result.Succeeded);
            return (await UserManager.FindByEmailAsync(email))!;
        }

        public async ValueTask DisposeAsync()
        {
            await serviceProvider.DisposeAsync();
        }
    }
}
