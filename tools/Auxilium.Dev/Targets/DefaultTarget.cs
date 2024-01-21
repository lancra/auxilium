namespace Auxilium.Dev.Targets;

internal class DefaultTarget : ITarget
{
    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            DevTargets.Default,
            "Executes the complete build process.",
            Bullseye.Targets.DependsOn(DevTargets.Publish, DevTargets.Test));
}
