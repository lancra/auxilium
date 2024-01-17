namespace Auxilium.Build.Targets;

internal class DotnetTarget : IBuildTarget
{
    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            BuildTargets.Dotnet,
            "Builds the solution into a set of output binaries.",
            Bullseye.Targets.DependsOn(BuildTargets.Clean),
            async () => await DotnetCli.RunAsync("build", "/warnaserror")
            .ConfigureAwait(false));
}
