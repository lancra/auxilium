using Auxilium.Dev.Lint;
using static SimpleExec.Command;

namespace Auxilium.Dev.Targets.Lint;

internal class LintTarget : ITarget
{
    private static readonly bool FixLinterIssues = Environment.GetEnvironmentVariable("AUXILIUM_FIX_LINT") == "1";

    private readonly ILinterConfigurationReader _configurationReader;

    public LintTarget(ILinterConfigurationReader configurationReader)
    {
        _configurationReader = configurationReader;
    }

    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            LintTargets.Lint,
            "Flags stylistic and functional issues via static code analysis tools.",
            forEach: ParseLinters(),
            async linter => await RunAsync(linter.Name, FixLinterIssues ? linter.Fix ?? linter.Check : linter.Check)
                .ConfigureAwait(false));

    private IReadOnlyCollection<Linter> ParseLinters()
    {
        var result = _configurationReader.Read();
        return result.IsValid
            ? result.Value!.Linters
            : throw new InvalidOperationException(result.Error);
    }
}
