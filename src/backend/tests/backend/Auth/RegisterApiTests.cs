using System.Net;
using System.Net.Http.Json;
using api.App.Contracts.Auth;
using Microsoft.AspNetCore.Mvc;

namespace backend.Tests.Auth;

public sealed class RegisterApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient client;
    private readonly CustomWebApplicationFactory factory;

    public RegisterApiTests(CustomWebApplicationFactory factory)
    {
        this.factory = factory;
        client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_WithValidPayload_CreatesUnconfirmedAccountAndSendsConfirmationCode()
    {
        var request = new RegisterRequest
        {
            Email = "valid@example.com",
            Password = "StrongPass1!",
            ConfirmPassword = "StrongPass1!"
        };

        var response = await client.PostAsJsonAsync("/auth/register", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.False(response.Headers.TryGetValues("Set-Cookie", out _));

        var payload = await response.Content.ReadFromJsonAsync<RegisterResponse>();
        var createdUser = await factory.FindUserByEmailAsync(request.Email);
        var codes = await factory.GetCodesForUserAsync(request.Email);

        Assert.NotNull(payload);
        Assert.Equal(request.Email, payload.Email);
        Assert.True(payload.RequiresEmailConfirmation);
        Assert.Equal("pending_email_confirmation", payload.Status);
        Assert.NotNull(createdUser);
        Assert.Equal(request.Email, createdUser.UserName);
        Assert.False(createdUser.EmailConfirmed);
        Assert.Single(codes);
        Assert.Equal(6, codes[0].Code.Length);
        Assert.Single(factory.EmailDeliveryService.Deliveries, delivery => delivery.Email == request.Email);
    }

    [Fact]
    public async Task Register_WithInvalidEmail_ReturnsValidationErrorAndDoesNotCreateAccount()
    {
        var request = new RegisterRequest
        {
            Email = "invalid-email",
            Password = "StrongPass1!",
            ConfirmPassword = "StrongPass1!"
        };

        var response = await client.PostAsJsonAsync("/auth/register", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        Assert.NotNull(payload);
        Assert.True(payload.Errors.ContainsKey("email"));
        Assert.False(response.Headers.TryGetValues("Set-Cookie", out _));
        Assert.Null(await factory.FindUserByEmailAsync(request.Email));
    }

    [Fact]
    public async Task Login_WithUnconfirmedLocalAccount_ReturnsPendingConfirmationAndDoesNotAuthenticate()
    {
        var request = new RegisterRequest
        {
            Email = "pending-login@example.com",
            Password = "StrongPass1!",
            ConfirmPassword = "StrongPass1!"
        };

        await client.PostAsJsonAsync("/auth/register", request);

        var loginResponse = await client.PostAsJsonAsync("/auth/login", new LoginRequest
        {
            Email = request.Email,
            Password = request.Password
        });

        Assert.Equal(HttpStatusCode.Forbidden, loginResponse.StatusCode);
        Assert.False(loginResponse.Headers.TryGetValues("Set-Cookie", out _));

        var payload = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();

        Assert.NotNull(payload);
        Assert.True(payload.RequiresEmailConfirmation);
        Assert.Equal("pending_email_confirmation", payload.Status);
    }

    [Fact]
    public async Task ResendEmailConfirmation_WithPendingAccount_GeneratesNewCodeAndSendsEmail()
    {
        var email = "resend-api@example.com";
        await client.PostAsJsonAsync("/auth/register", new RegisterRequest
        {
            Email = email,
            Password = "StrongPass1!",
            ConfirmPassword = "StrongPass1!"
        });

        factory.TimeProvider.Advance(TimeSpan.FromMinutes(2));

        var response = await client.PostAsJsonAsync("/auth/email-confirmation/resend", new ResendEmailConfirmationRequest
        {
            Email = email
        });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<ResendEmailConfirmationResponse>();
        var codes = await factory.GetCodesForUserAsync(email);

        Assert.NotNull(payload);
        Assert.Equal("pending_email_confirmation", payload.Status);
        Assert.Equal(2, codes.Count);
        Assert.NotNull(codes[0].InvalidatedAtUtc);
        Assert.NotEqual(codes[0].Code, codes[1].Code);
        Assert.Equal(2, factory.EmailDeliveryService.Deliveries.Count(delivery => delivery.Email == email));
    }

    [Fact]
    public async Task ResendEmailConfirmation_WithConfirmedAccount_ReturnsValidationError()
    {
        var email = "confirmed-api@example.com";
        await client.PostAsJsonAsync("/auth/register", new RegisterRequest
        {
            Email = email,
            Password = "StrongPass1!",
            ConfirmPassword = "StrongPass1!"
        });

        var initialCode = (await factory.GetCodesForUserAsync(email)).Single().Code;
        await client.PostAsJsonAsync("/auth/email-confirmation/confirm", new ConfirmEmailRequest
        {
            Email = email,
            Code = initialCode
        });

        var response = await client.PostAsJsonAsync("/auth/email-confirmation/resend", new ResendEmailConfirmationRequest
        {
            Email = email
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var payload = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(payload);
        Assert.True(payload.Errors.ContainsKey("email"));
    }

    [Fact]
    public async Task ResendAvailability_WithActiveCooldown_ReturnsRemainingSeconds()
    {
        var email = "cooldown-api@example.com";
        await client.PostAsJsonAsync("/auth/register", new RegisterRequest
        {
            Email = email,
            Password = "StrongPass1!",
            ConfirmPassword = "StrongPass1!"
        });

        factory.TimeProvider.Advance(TimeSpan.FromSeconds(12));

        var response = await client.PostAsJsonAsync("/auth/email-confirmation/resend-availability",
            new EmailConfirmationResendAvailabilityRequest
            {
                Email = email
            });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<EmailConfirmationResendAvailabilityResponse>();
        Assert.NotNull(payload);
        Assert.False(payload.CanResend);
        Assert.Equal(48, payload.RemainingSeconds);
    }

    [Fact]
    public async Task ResendAvailability_AfterCooldown_ReturnsCanResend()
    {
        var email = "cooldown-ready-api@example.com";
        await client.PostAsJsonAsync("/auth/register", new RegisterRequest
        {
            Email = email,
            Password = "StrongPass1!",
            ConfirmPassword = "StrongPass1!"
        });

        factory.TimeProvider.Advance(TimeSpan.FromMinutes(1));

        var response = await client.PostAsJsonAsync("/auth/email-confirmation/resend-availability",
            new EmailConfirmationResendAvailabilityRequest
            {
                Email = email
            });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<EmailConfirmationResendAvailabilityResponse>();
        Assert.NotNull(payload);
        Assert.True(payload.CanResend);
        Assert.Equal(0, payload.RemainingSeconds);
    }

    [Fact]
    public async Task ConfirmEmail_WithValidCode_MarksAccountAsConfirmed()
    {
        var email = "confirm-api@example.com";
        await client.PostAsJsonAsync("/auth/register", new RegisterRequest
        {
            Email = email,
            Password = "StrongPass1!",
            ConfirmPassword = "StrongPass1!"
        });

        var initialCode = (await factory.GetCodesForUserAsync(email)).Single().Code;
        var response = await client.PostAsJsonAsync("/auth/email-confirmation/confirm", new ConfirmEmailRequest
        {
            Email = email,
            Code = initialCode
        });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<ConfirmEmailResponse>();
        var user = await factory.FindUserByEmailAsync(email);

        Assert.NotNull(payload);
        Assert.Equal("confirmed", payload.Status);
        Assert.True(user!.EmailConfirmed);
    }

    [Fact]
    public async Task ConfirmEmail_WithInvalidCode_DoesNotConfirmAccount()
    {
        var email = "invalid-code-api@example.com";
        await client.PostAsJsonAsync("/auth/register", new RegisterRequest
        {
            Email = email,
            Password = "StrongPass1!",
            ConfirmPassword = "StrongPass1!"
        });

        var response = await client.PostAsJsonAsync("/auth/email-confirmation/confirm", new ConfirmEmailRequest
        {
            Email = email,
            Code = "ZZZ999"
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var payload = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        var user = await factory.FindUserByEmailAsync(email);
        Assert.NotNull(payload);
        Assert.True(payload.Errors.ContainsKey("code"));
        Assert.False(user!.EmailConfirmed);
    }

    [Fact]
    public async Task ConfirmEmail_WithExpiredCode_DoesNotConfirmAccount()
    {
        var email = "expired-api@example.com";
        await client.PostAsJsonAsync("/auth/register", new RegisterRequest
        {
            Email = email,
            Password = "StrongPass1!",
            ConfirmPassword = "StrongPass1!"
        });

        var initialCode = (await factory.GetCodesForUserAsync(email)).Single().Code;
        factory.TimeProvider.Advance(TimeSpan.FromMinutes(16));

        var response = await client.PostAsJsonAsync("/auth/email-confirmation/confirm", new ConfirmEmailRequest
        {
            Email = email,
            Code = initialCode
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var user = await factory.FindUserByEmailAsync(email);
        Assert.False(user!.EmailConfirmed);
    }

    [Fact]
    public async Task ConfirmEmail_WithSupersededCode_DoesNotConfirmAccount()
    {
        var email = "superseded-api@example.com";
        await client.PostAsJsonAsync("/auth/register", new RegisterRequest
        {
            Email = email,
            Password = "StrongPass1!",
            ConfirmPassword = "StrongPass1!"
        });

        var firstCode = (await factory.GetCodesForUserAsync(email)).Single().Code;
        factory.TimeProvider.Advance(TimeSpan.FromMinutes(2));
        await client.PostAsJsonAsync("/auth/email-confirmation/resend", new ResendEmailConfirmationRequest
        {
            Email = email
        });

        var response = await client.PostAsJsonAsync("/auth/email-confirmation/confirm", new ConfirmEmailRequest
        {
            Email = email,
            Code = firstCode
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var user = await factory.FindUserByEmailAsync(email);
        Assert.False(user!.EmailConfirmed);
    }

    [Fact]
    public async Task ConfirmEmail_AfterSuccess_AllowsFullLogin()
    {
        var email = "full-login@example.com";
        const string password = "StrongPass1!";

        await client.PostAsJsonAsync("/auth/register", new RegisterRequest
        {
            Email = email,
            Password = password,
            ConfirmPassword = password
        });

        var initialCode = (await factory.GetCodesForUserAsync(email)).Single().Code;
        await client.PostAsJsonAsync("/auth/email-confirmation/confirm", new ConfirmEmailRequest
        {
            Email = email,
            Code = initialCode
        });

        var response = await client.PostAsJsonAsync("/auth/login", new LoginRequest
        {
            Email = email,
            Password = password
        });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(response.Headers.TryGetValues("Set-Cookie", out var setCookieValues));
        Assert.Contains(setCookieValues, header => header.Contains(".AspNetCore.Identity.Application=", StringComparison.Ordinal));

        var payload = await response.Content.ReadFromJsonAsync<LoginResponse>();
        Assert.NotNull(payload);
        Assert.False(payload.RequiresEmailConfirmation);
        Assert.Equal("authenticated", payload.Status);
    }

    [Fact]
    public async Task Logout_AfterAuthenticatedLogin_ClearsCurrentSession()
    {
        var email = "logout-api@example.com";
        const string password = "StrongPass1!";

        await client.PostAsJsonAsync("/auth/register", new RegisterRequest
        {
            Email = email,
            Password = password,
            ConfirmPassword = password
        });

        var initialCode = (await factory.GetCodesForUserAsync(email)).Single().Code;
        await client.PostAsJsonAsync("/auth/email-confirmation/confirm", new ConfirmEmailRequest
        {
            Email = email,
            Code = initialCode
        });

        var loginResponse = await client.PostAsJsonAsync("/auth/login", new LoginRequest
        {
            Email = email,
            Password = password
        });

        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        var logoutResponse = await client.PostAsync("/auth/logout", content: null);

        Assert.Equal(HttpStatusCode.NoContent, logoutResponse.StatusCode);
        Assert.True(logoutResponse.Headers.TryGetValues("Set-Cookie", out var setCookieValues));
        Assert.Contains(setCookieValues, header =>
            header.Contains(".AspNetCore.Identity.Application=;", StringComparison.Ordinal)
            && header.Contains("expires=", StringComparison.OrdinalIgnoreCase));

        var secondLogoutResponse = await client.PostAsync("/auth/logout", content: null);
        Assert.Equal(HttpStatusCode.Unauthorized, secondLogoutResponse.StatusCode);
    }
}
