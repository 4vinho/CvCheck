using System.ComponentModel.DataAnnotations;
using api.App.Contracts.Auth;
using api.Core.Results.Auth;
using api.Infra.Auth;
using api.Infra.Services.Auth;
using Microsoft.AspNetCore.Identity;

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
    public async Task RegistrationService_SetsEmailAsPrimaryIdentifier()
    {
        var userManager = TestUserManagerFactory.Create();
        var service = new RegistrationService(userManager);
        var request = new RegisterRequest
        {
            Email = "primary@example.com",
            Password = "StrongPass1!",
            ConfirmPassword = "StrongPass1!"
        };

        var result = await service.RegisterAsync(request);

        Assert.True(result.Succeeded);
        var user = await userManager.FindByEmailAsync(request.Email);
        Assert.NotNull(user);
        Assert.Equal(request.Email, user.UserName);
    }
}
