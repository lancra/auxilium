using static SimpleExec.Command;

namespace Auxilium.Build.Targets;

internal class LintTarget : IBuildTarget
{
    private static readonly bool FixLinterIssues = Environment.GetEnvironmentVariable("AUXILIUM_FIX_LINT") == "1";

    private static readonly Linter[] Linters = [
        new("markdownlint", "markdownlint", "."),
        new("prettier", "prettier", "--check .", "--write ."),
        new("typos", "typos", ".", "--write-changes ."),
        new("yamllint", "yamllint", "--strict .")];

    public void Setup(Bullseye.Targets targets)
        => targets.Add(
            BuildTargets.Lint,
            "Flags stylistic and functional issues via static code analysis tools.",
            forEach: Linters,
            async linter => await RunAsync(linter.Executable, FixLinterIssues ? linter.FixArguments : linter.CheckArguments)
                .ConfigureAwait(false));
}
