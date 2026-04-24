using MediatR;

namespace CvCheck.Application.Platform.Ping;

public sealed class PingQueryHandler : IRequestHandler<PingQuery, PingResult>
{
    public Task<PingResult> Handle(PingQuery request, CancellationToken cancellationToken) =>
        Task.FromResult(new PingResult(request.Message.Trim()));
}
