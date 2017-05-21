using Autofac;
using OctoPoC.Core.Environments;
using OctoPoC.Core.Environments.DeploymentTargets;
using OctoPoC.Core.EventStore;
using OctoPoC.Core.Projects;
using OctoPoC.Core.Projects.Settings;

namespace OctoPoC.Core
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryEventStore>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<OctopusSystemActor>();
            builder.RegisterType<EnvironmentActor>();
            builder.RegisterType<CloudRegionActor>();
            builder.RegisterType<ListeningTentacleActor>();
            builder.RegisterType<AppSettingActor>();
            builder.RegisterType<ProjectActor>();
            builder.RegisterType<InMemoryEnvironmentRepo>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<InMemoryTargetLoader>().AsImplementedInterfaces().SingleInstance();
        }
    }
}
