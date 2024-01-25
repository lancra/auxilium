using Autofac;
using Auxilium.Dev.Lint;
using Auxilium.Dev.Targets;

namespace Auxilium.Dev;

internal static class DevStartup
{
    public static IContainer BuildContainer()
    {
        var builder = new ContainerBuilder();

        builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
            .AssignableTo<ITarget>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterType<FileLinterConfigurationReader>()
            .As<ILinterConfigurationReader>()
            .InstancePerLifetimeScope();

        return builder.Build();
    }
}
