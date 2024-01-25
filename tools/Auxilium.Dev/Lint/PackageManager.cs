namespace Auxilium.Dev.Lint;

internal record PackageManager(PackageManagerName Name, Uri Url, string Install, IReadOnlyCollection<PackageManagerPlatform> Platforms)
{
    public string? Source { get; init; }
}
