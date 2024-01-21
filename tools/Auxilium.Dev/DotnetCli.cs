namespace Auxilium.Dev;

internal static class DotnetCli
{
    public static async Task RunAsync(string command, params string[] arguments)
    {
        var argumentsText = string.Empty;
        if (arguments.Length != 0)
        {
            argumentsText = $" {string.Join(' ', arguments)}";
        }

        await SimpleExec.Command.RunAsync("dotnet", $"{command} --configuration Release --verbosity minimal --nologo{argumentsText}")
            .ConfigureAwait(false);
    }
}
