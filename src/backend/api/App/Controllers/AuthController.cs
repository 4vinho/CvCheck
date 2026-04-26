using api.App.Contracts.Auth;
using api.Core.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.App.Controllers;

[ApiController]
[Route("auth")]
public sealed class AuthController(
    IRegistrationService registrationService,
    ILoginService loginService,
    ILogoutService logoutService,
    IEmailConfirmationService emailConfirmationService) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType<RegisterResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RegisterResponse>> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var result = await registrationService.RegisterAsync(request, cancellationToken);
        if (!result.Succeeded)
        {
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>(result.Errors))
            {
                Status = StatusCodes.Status400BadRequest
            });
        }

        var response = new RegisterResponse(
            result.Email!,
            RequiresEmailConfirmation: true,
            Status: "pending_email_confirmation");

        return Created(string.Empty, response);
    }

    [HttpPost("login")]
    [ProducesResponseType<LoginResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<LoginResponse>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoginResponse>> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var result = await loginService.LoginAsync(request, cancellationToken);
        if (result.Succeeded)
        {
            return Ok(new LoginResponse(result.Email!, false, "authenticated"));
        }

        if (result.RequiresEmailConfirmation)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new LoginResponse(
                result.Email!,
                true,
                "pending_email_confirmation"));
        }

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>(result.Errors))
        {
            Status = StatusCodes.Status400BadRequest
        });
    }

    [Authorize]
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        await logoutService.LogoutAsync(cancellationToken);
        return NoContent();
    }

    [HttpPost("email-confirmation/resend")]
    [ProducesResponseType<ResendEmailConfirmationResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ResendEmailConfirmationResponse>> ResendEmailConfirmation(
        [FromBody] ResendEmailConfirmationRequest request,
        CancellationToken cancellationToken)
    {
        var result = await emailConfirmationService.ResendAsync(request.Email, cancellationToken);
        if (!result.Succeeded)
        {
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>(result.Errors))
            {
                Status = StatusCodes.Status400BadRequest
            });
        }

        return Ok(new ResendEmailConfirmationResponse(request.Email, "pending_email_confirmation"));
    }

    [HttpPost("email-confirmation/resend-availability")]
    [ProducesResponseType<EmailConfirmationResendAvailabilityResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmailConfirmationResendAvailabilityResponse>> GetResendAvailability(
        [FromBody] EmailConfirmationResendAvailabilityRequest request,
        CancellationToken cancellationToken)
    {
        var result = await emailConfirmationService.GetResendAvailabilityAsync(request.Email, cancellationToken);
        if (!result.Succeeded)
        {
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>(result.Errors))
            {
                Status = StatusCodes.Status400BadRequest
            });
        }

        return Ok(new EmailConfirmationResendAvailabilityResponse(
            request.Email,
            result.CanResend,
            result.RemainingSeconds));
    }

    [HttpPost("email-confirmation/confirm")]
    [ProducesResponseType<ConfirmEmailResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ConfirmEmailResponse>> ConfirmEmail(
        [FromBody] ConfirmEmailRequest request,
        CancellationToken cancellationToken)
    {
        var result = await emailConfirmationService.ConfirmAsync(request.Email, request.Code, cancellationToken);
        if (!result.Succeeded)
        {
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>(result.Errors))
            {
                Status = StatusCodes.Status400BadRequest
            });
        }

        return Ok(new ConfirmEmailResponse(request.Email, "confirmed"));
    }
}
