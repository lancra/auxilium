namespace Auxilium.Dev.Lint;

internal record Linter(string Name, Uri Url, string Check, IReadOnlyCollection<LinterPackage> Packages)
{
    public string? Fix { get; init; }

    public override string ToString() => Name;
}
