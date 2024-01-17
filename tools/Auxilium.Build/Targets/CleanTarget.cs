namespace Auxilium.Build.Targets;

internal class CleanTarget : IBuildTarget
{
    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            BuildTargets.Clean,
            "Cleans build artifacts from prior executions.",
            async () => await DotnetCli.RunAsync("clean")
            .ConfigureAwait(false));
}
