namespace Auxilium.Dev.Lint;

internal record LinterConfiguration(IReadOnlyCollection<PackageManager> PackageManagers, IReadOnlyCollection<Linter> Linters)
{
}
