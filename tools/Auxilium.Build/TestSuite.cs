namespace Auxilium.Build;

internal record TestSuite(string Name, string Description, TestProject[] Projects)
{
    public override string ToString() => Name;
}
