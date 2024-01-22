namespace Auxilium.Dev.Targets.Build;

internal class CleanTarget : ITarget
{
    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            BuildTargets.Clean,
            "Cleans build artifacts from prior executions.",
            async () => await DotnetCli.RunAsync("clean")
            .ConfigureAwait(false));
}
