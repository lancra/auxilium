namespace Auxilium.Dev.Targets;

internal class CleanTarget : ITarget
{
    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            DevTargets.Clean,
            "Cleans build artifacts from prior executions.",
            async () => await DotnetCli.RunAsync("clean")
            .ConfigureAwait(false));
}
