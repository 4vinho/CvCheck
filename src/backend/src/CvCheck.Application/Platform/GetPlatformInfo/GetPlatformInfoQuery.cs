using MediatR;

namespace CvCheck.Application.Platform.GetPlatformInfo;

public sealed record GetPlatformInfoQuery : IRequest<PlatformInfoResult>;
