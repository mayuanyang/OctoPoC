using Autofac;
using OctoPoC.Core.DeploymentTargets;
using OctoPoC.Core.EventStore;
using OctoPoC.Core.Projects.Settings;
using OctoPoC.Core.Servers;

namespace OctoPoC.Core
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryEventStore>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<OctopusServerActor>();
            builder.RegisterType<CloudRegionActor>();
            builder.RegisterType<ListeningTentacleActor>();
            builder.RegisterType<AppSettingActor>();
        }
    }
}
