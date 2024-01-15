namespace Auxilium.Build.Targets;

/// <summary>
/// Represents a target execution within the build process.
/// </summary>
internal interface IBuildTarget
{
    /// <summary>
    /// Performs setup necessary to include the target into a collection.
    /// </summary>
    /// <param name="targets">The target collection to modify.</param>
    void Setup(Bullseye.Targets targets);
}
