namespace Auxilium.Dev.Targets.Build;

internal class DotnetTarget : ITarget
{
    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            BuildTargets.Dotnet,
            "Builds the solution into a set of output binaries.",
            Bullseye.Targets.DependsOn(BuildTargets.Clean),
            async () => await DotnetCli.RunAsync("build", "/warnaserror")
            .ConfigureAwait(false));
}
