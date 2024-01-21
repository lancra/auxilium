using Autofac;
using Auxilium.Dev;
using Auxilium.Dev.Targets;
using SimpleExec;

var container = DevStartup.BuildContainer();

var targets = new Bullseye.Targets();
foreach (var target in container.Resolve<IEnumerable<ITarget>>())
{
    target.Setup(targets);
}

await targets.RunAndExitAsync(args, messageOnly: ex => ex is ExitCodeException)
    .ConfigureAwait(false);
