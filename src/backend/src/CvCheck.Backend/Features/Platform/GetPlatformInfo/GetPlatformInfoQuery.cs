using MediatR;

namespace CvCheck.Backend.Features.Platform.GetPlatformInfo;

public sealed record GetPlatformInfoQuery : IRequest<PlatformInfoResult>;
