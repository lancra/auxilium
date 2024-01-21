namespace Auxilium.Dev;

internal record TestSuite(string Name, string Description, TestProject[] Projects)
{
    public override string ToString() => Name;
}
