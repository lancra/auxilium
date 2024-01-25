namespace Auxilium.Dev.Lint;

/// <summary>
/// Represents a reader for linter configuration.
/// </summary>
internal interface ILinterConfigurationReader
{
    /// <summary>
    /// Reads the linter configuration.
    /// </summary>
    /// <returns>The result of the read operation.</returns>
    LinterConfigurationResult Read();
}
