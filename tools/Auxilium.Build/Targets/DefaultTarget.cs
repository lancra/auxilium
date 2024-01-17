namespace Auxilium.Build.Targets;

internal class DefaultTarget : IBuildTarget
{
    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            BuildTargets.Default,
            "Executes the complete build process.",
            Bullseye.Targets.DependsOn(BuildTargets.Publish, BuildTargets.Test));
}
