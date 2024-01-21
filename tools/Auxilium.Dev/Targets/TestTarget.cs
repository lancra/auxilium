namespace Auxilium.Dev.Targets;

internal class TestTarget : ITarget
{
    private static readonly TestSuite[] Suites = [
        new(
            DevTargets.TestIntegration,
            "Tests integrations between components of the system.",
            [new("integration", "tests/IntegrationTests"),]),
        new(
            DevTargets.TestUnit,
            "Tests individual components of the system.",
            [new("api", "tests/Api.Facts"), new("domain", "tests/Domain.Facts"),]),
    ];

    public void Setup(Bullseye.Targets targets)
    {
        foreach (var suite in Suites)
        {
            targets.Add(
                suite.Name,
                suite.Description,
                Bullseye.Targets.DependsOn(DevTargets.Dotnet),
                forEach: suite.Projects,
                async project => await DotnetCli
                    .RunAsync(
                        $"test {project.Path}",
                        "--no-build",
                        $"--results-directory {string.Format(ArtifactPaths.TestResultFormat, project.Name)}",
                        "--collect \"XPlat Code Coverage\"",
                        "--logger trx")
                    .ConfigureAwait(false));
        }

        targets.Add(
            DevTargets.Test,
            "Executes automated test suites.",
            Bullseye.Targets.DependsOn(Suites.Select(s => s.Name)
                .ToArray()));
    }
}
