namespace Auxilium.Dev.Targets.Build;

internal class PublishTarget : ITarget
{
    private static readonly ExecutableProject[] Projects = [new("api", "src/Api")];

    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            BuildTargets.Publish,
            "Publishes executable projects and dependencies to a folder for deployment.",
            Bullseye.Targets.DependsOn(BuildTargets.Dotnet),
            forEach: Projects,
            async project => await DotnetCli
                .RunAsync(
                    $"publish {project.Path}",
                    "--no-build",
                    $"--output {string.Format(ArtifactPaths.PublishedExecutableFormat, project.Name)}")
                .ConfigureAwait(false));
}
