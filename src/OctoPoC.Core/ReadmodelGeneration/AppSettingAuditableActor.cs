using System;
using Akka.Actor;
using OctoPoC.Messages.Events;

namespace OctoPoC.Core.ReadmodelGeneration
{
    class AppSettingAuditableActor : ReceiveActor
    {
        public AppSettingAuditableActor(IAuditableSettingsRepo repo)
        {
            Receive<AppSettingAddedEvent>(x =>
            {
                repo.Add(x);
                Console.WriteLine($"AppSetting for Project: {x.ProjectId} Key: {x.Key} Value: {x.Value} is added to read model: AuditableSettingsRepo" );
            });

            Receive<AppSettingUpdatedEvent>(x =>
            {
                repo.Update(x);
                Console.WriteLine($"AppSetting for Project: {x.ProjectId} Key: {x.Key} Value: {x.Value} is updated in read model: AuditableSettingsRepo");
            });
        }
    }
}
