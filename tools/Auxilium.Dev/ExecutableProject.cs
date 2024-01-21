namespace Auxilium.Dev;

internal record ExecutableProject(string Name, string Path)
{
    public override string ToString() => Name;
}
