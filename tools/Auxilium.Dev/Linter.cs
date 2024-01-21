namespace Auxilium.Dev;

internal record Linter(string Name, string Executable, string CheckArguments, string FixArguments)
{
    public Linter(string name, string executable, string checkArguments)
        : this(name, executable, checkArguments, checkArguments)
    {
    }

    public override string ToString() => Name;
}
