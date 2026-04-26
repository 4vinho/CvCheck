using api.App.Contracts.Auth;
using api.Core.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;

namespace api.App.Controllers;

[ApiController]
[Route("auth")]
public sealed class AuthController(IRegistrationService registrationService) : ControllerBase
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
}
