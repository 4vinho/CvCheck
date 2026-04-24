using MediatR;

namespace CvCheck.Application.Platform.Ping;

public sealed record PingQuery(string Message) : IRequest<PingResult>;
