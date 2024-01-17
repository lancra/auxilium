namespace Auxilium.Build;

internal record ExecutableProject(string Name, string Path)
{
    public override string ToString() => Name;
}
