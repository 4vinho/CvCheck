using CvCheck.Api.Contracts;
using CvCheck.Application.Platform.GetPlatformInfo;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CvCheck.Api.Controllers;

[ApiController]
[Route("api/platform")]
public sealed class PlatformController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PlatformInfoResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<PlatformInfoResponse>> GetAsync(CancellationToken cancellationToken)
    {
        var platformInfo = await mediator.Send(new GetPlatformInfoQuery(), cancellationToken);

        return Ok(platformInfo.Adapt<PlatformInfoResponse>());
    }
}
