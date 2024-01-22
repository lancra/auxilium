namespace Auxilium.Dev.Targets.Build;

internal class BuildTarget : ITarget
{
    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            BuildTargets.Build,
            "Executes the complete build process.",
            Bullseye.Targets.DependsOn(BuildTargets.Publish, BuildTargets.Test));
}
