using Autofac;
using Auxilium.Build;
using Auxilium.Build.Targets;
using SimpleExec;

var container = BuildStartup.BuildContainer();

var targets = new Bullseye.Targets();
foreach (var buildTarget in container.Resolve<IEnumerable<IBuildTarget>>())
{
    buildTarget.Setup(targets);
}

await targets.RunAndExitAsync(args, messageOnly: ex => ex is ExitCodeException)
    .ConfigureAwait(false);
