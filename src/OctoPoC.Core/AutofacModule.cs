using Autofac;
using OctoPoC.Core.DeploymentTargets;
using OctoPoC.Core.Environments;
using OctoPoC.Core.PowerShell;
using OctoPoC.Core.Projects;
using OctoPoC.Core.ReadmodelGeneration;
using OctoPoC.Core.Settings;
using OctoPoC.Core.Websites;

namespace OctoPoC.Core
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EnvironmentActor>();
            builder.RegisterType<CloudRegionActor>();
            builder.RegisterType<ListeningTentacleActor>();
            builder.RegisterType<SettingActor>();
            builder.RegisterType<AppSettingAuditableActor>();
            builder.RegisterType<AppSettingNonAuditableActor>();
            builder.RegisterType<ProjectActor>();
            builder.RegisterType<PowerShellActor>();
            builder.RegisterType<DeployWebsiteActor>();
            builder.RegisterType<InMemoryEnvironmentRepo>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<InMemoryTargetLoader>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<AuditableSettingsRepo>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<NonAuditableSettingsRepo>().AsImplementedInterfaces().SingleInstance();
        }
    }
}
