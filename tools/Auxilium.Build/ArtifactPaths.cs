namespace Auxilium.Build;

internal static class ArtifactPaths
{
    public const string Root = "artifacts";

    public const string PublishedExecutables = $"{Root}/publish";
    public const string PublishedExecutableFormat = $"{PublishedExecutables}/{{0}}";

    public const string TestResults = $"{Root}/test-results";
    public const string TestResultFormat = $"{TestResults}/{{0}}";
}
