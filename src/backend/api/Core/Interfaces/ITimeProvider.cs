namespace api.Core.Interfaces;

public interface ITimeProvider
{
    DateTimeOffset UtcNow { get; }
}
