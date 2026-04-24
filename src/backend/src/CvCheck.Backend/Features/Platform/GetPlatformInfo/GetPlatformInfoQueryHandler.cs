using CvCheck.Backend.Abstractions;
using MediatR;

namespace CvCheck.Backend.Features.Platform.GetPlatformInfo;

public sealed class GetPlatformInfoQueryHandler(
    IPlatformInfoProvider platformInfoProvider) : IRequestHandler<GetPlatformInfoQuery, PlatformInfoResult>
{
    public Task<PlatformInfoResult> Handle(
        GetPlatformInfoQuery request,
        CancellationToken cancellationToken) =>
        platformInfoProvider.GetAsync(cancellationToken);
}
