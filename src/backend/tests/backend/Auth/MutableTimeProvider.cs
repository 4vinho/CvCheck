using api.Core.Interfaces;

namespace backend.Tests.Auth;

public sealed class MutableTimeProvider(DateTimeOffset utcNow) : ITimeProvider
{
    public DateTimeOffset UtcNow { get; private set; } = utcNow;

    public void Advance(TimeSpan offset)
    {
        UtcNow = UtcNow.Add(offset);
    }
}
