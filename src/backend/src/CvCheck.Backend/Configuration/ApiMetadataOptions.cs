namespace CvCheck.Backend.Configuration;

public sealed class ApiMetadataOptions
{
    public const string SectionName = "ApiMetadata";

    public string Name { get; init; } = "CvCheck API";

    public string Version { get; init; } = "v1";

    public string Description { get; init; } = "Initial backend foundation for the CvCheck platform.";
}
