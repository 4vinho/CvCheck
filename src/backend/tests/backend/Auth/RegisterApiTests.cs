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
    public async Task Register_WithValidPayload_CreatesUnconfirmedAccount()
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

        Assert.NotNull(payload);
        Assert.Equal(request.Email, payload.Email);
        Assert.True(payload.RequiresEmailConfirmation);
        Assert.Equal("pending_email_confirmation", payload.Status);

        var createdUser = await factory.FindUserByEmailAsync(request.Email);

        Assert.NotNull(createdUser);
        Assert.Equal(request.Email, createdUser.UserName);
        Assert.False(createdUser.EmailConfirmed);
    }

    [Fact]
    public async Task Register_WithInvalidEmail_ReturnsValidationError()
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
    }

    [Fact]
    public async Task Register_WithWeakPassword_ReturnsIdentityValidation()
    {
        var request = new RegisterRequest
        {
            Email = "weakpass@example.com",
            Password = "123",
            ConfirmPassword = "123"
        };

        var response = await client.PostAsJsonAsync("/auth/register", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        Assert.NotNull(payload);
        Assert.True(payload.Errors.ContainsKey("password"));
    }

    [Fact]
    public async Task Register_WithDifferentConfirmPassword_ReturnsValidationError()
    {
        var request = new RegisterRequest
        {
            Email = "mismatch@example.com",
            Password = "StrongPass1!",
            ConfirmPassword = "StrongPass2!"
        };

        var response = await client.PostAsJsonAsync("/auth/register", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        Assert.NotNull(payload);
        Assert.True(payload.Errors.ContainsKey("confirmPassword"));
    }

    [Fact]
    public async Task Register_WithExistingEmail_ReturnsValidation()
    {
        var request = new RegisterRequest
        {
            Email = "duplicate@example.com",
            Password = "StrongPass1!",
            ConfirmPassword = "StrongPass1!"
        };

        var firstResponse = await client.PostAsJsonAsync("/auth/register", request);
        var secondResponse = await client.PostAsJsonAsync("/auth/register", request);

        Assert.Equal(HttpStatusCode.Created, firstResponse.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, secondResponse.StatusCode);

        var payload = await secondResponse.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        Assert.NotNull(payload);
        Assert.True(payload.Errors.ContainsKey("email"));
    }
}
