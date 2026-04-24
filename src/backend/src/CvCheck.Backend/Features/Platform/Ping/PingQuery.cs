using MediatR;

namespace CvCheck.Backend.Features.Platform.Ping;

public sealed record PingQuery(string Message) : IRequest<PingResult>;
