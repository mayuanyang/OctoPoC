using System;
using Akka.Actor;
using OctoPoC.Messages.Events;

namespace OctoPoC.Core.Projects.ReadmodelGeneration
{
    class AppSettingReadmodelActor : ReceiveActor
    {
        public AppSettingReadmodelActor()
        {
            Receive<AppSettingAddedEvent>(x =>
            {
                Console.WriteLine($"AppSetting for Project: {x.ProjectId} Key: {x.Key} Value: {x.Value} is added" );
            });

            Receive<AppSettingUpdatedEvent>(x =>
            {
                Console.WriteLine($"AppSetting for Project: {x.ProjectId} Key: {x.Key} Value: {x.Value} is updated");
            });
        }
    }
}
