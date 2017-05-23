using Akka.Actor;
using OctoPoC.Messages.Commands;
using OctoPoC.Messages.Events;

namespace OctoPoC.Core.Settings
{
    class SettingActor : ReceiveActor
    {
        public SettingActor()
        {
            Receive<AddAppSettingCommand>(x =>
            {
                // Behavior, nothing at this moment

                // Report back
                Sender.Tell(new AppSettingAddedEvent(x.ProjectId, x.Key, x.Value, x.Version, x.RecordTime));
            });

            Receive<UpdateAppSettingCommand>(x =>
            {
                // Behavior, nothing at this moment

                // Report back
                Sender.Tell(new AppSettingUpdatedEvent(x.ProjectId, x.Key, x.Value, x.Version, x.RecordTime));

            });
        }
    }
}
