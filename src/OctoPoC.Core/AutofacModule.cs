using Autofac;
using OctoPoC.Core.DeploymentTargets;
using OctoPoC.Core.Environments;
using OctoPoC.Core.EventStore;
using OctoPoC.Core.PowerShell;
using OctoPoC.Core.Projects;
using OctoPoC.Core.Settings;
using OctoPoC.Core.Websites;

namespace OctoPoC.Core
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryEventStore>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<EnvironmentActor>();
            builder.RegisterType<CloudRegionActor>();
            builder.RegisterType<ListeningTentacleActor>();
            builder.RegisterType<AppSettingActor>();
            builder.RegisterType<ProjectActor>();
            builder.RegisterType<PowerShellActor>();
            builder.RegisterType<DeployWebsiteActor>();
            builder.RegisterType<InMemoryEnvironmentRepo>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<InMemoryTargetLoader>().AsImplementedInterfaces().SingleInstance();
        }
    }
}
