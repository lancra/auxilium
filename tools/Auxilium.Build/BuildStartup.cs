using Autofac;
using Auxilium.Build.Targets;

namespace Auxilium.Build;

internal static class BuildStartup
{
    public static IContainer BuildContainer()
    {
        var builder = new ContainerBuilder();

        builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
            .AssignableTo<IBuildTarget>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        return builder.Build();
    }
}
