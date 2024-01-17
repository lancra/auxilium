namespace Auxilium.Build;

internal record TestProject(string Name, string Path)
{
    public override string ToString() => Name;
}
