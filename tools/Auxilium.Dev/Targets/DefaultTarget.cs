namespace Auxilium.Dev.Targets;

internal class DefaultTarget : ITarget
{
    public void Setup(Bullseye.Targets targets)
        => targets.Add("default", dependsOn: [BuildTargets.Build]);
}
