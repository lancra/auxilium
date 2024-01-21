namespace Auxilium.Dev.Targets;

internal class DotnetTarget : ITarget
{
    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            DevTargets.Dotnet,
            "Builds the solution into a set of output binaries.",
            Bullseye.Targets.DependsOn(DevTargets.Clean),
            async () => await DotnetCli.RunAsync("build", "/warnaserror")
            .ConfigureAwait(false));
}
