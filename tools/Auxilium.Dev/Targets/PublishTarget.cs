namespace Auxilium.Dev.Targets;

internal class PublishTarget : ITarget
{
    private static readonly ExecutableProject[] Projects = [new("api", "src/Api")];

    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            DevTargets.Publish,
            "Publishes executable projects and dependencies to a folder for deployment.",
            Bullseye.Targets.DependsOn(DevTargets.Dotnet),
            forEach: Projects,
            async project => await DotnetCli
                .RunAsync(
                    $"publish {project.Path}",
                    "--no-build",
                    $"--output {string.Format(ArtifactPaths.PublishedExecutableFormat, project.Name)}")
                .ConfigureAwait(false));
}
