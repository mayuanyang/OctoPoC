using System;
using Akka.Actor;
using Akka.DI.Core;
using Akka.Persistence;
using OctoPoC.Core.EventStore;
using OctoPoC.Core.Websites;
using OctoPoC.Messages.Commands;
using OctoPoC.Messages.Events;

namespace OctoPoC.Core.Projects
{
    public class ProjectActor : PersistentActor
    {
        private IActorRef _sender;
        private string _currentVersion = "";
        private IActorRef _websiteActor;

        public ProjectActor()
        {
            PersistenceId = "30a37555-780a-4748-962c-26a4d163f56c";
            _websiteActor = Context.ActorOf(Context.DI().Props<DeployWebsiteActor>(), "WebsiteActor");
        }
    
        protected override bool ReceiveRecover(object message)
        {
            if (message is EventRecord)
            {
                var evt = (EventRecord)message;
                if (evt.Event is WebsiteDeployedEvent)
                {
                     Apply((WebsiteDeployedEvent)evt.Event);
                }
                else if (evt.Event is AppSettingAddedEvent)
                {
                    Apply((AppSettingAddedEvent)evt.Event);
                }
            }
            return true;
        }

        protected override bool ReceiveCommand(object message)
        {
            var aggregateId = Guid.Parse(PersistenceId);
            if (message is DeployWebsiteCommand)
            {
                _sender = Sender;
                _websiteActor.Tell(message, Self);
            }
            else if (message is WebsiteDeployedEvent)
            {
                var evt = (WebsiteDeployedEvent) message;
                Persist<EventRecord>(new EventRecord(Guid.NewGuid(), 1, aggregateId, evt), x =>
                {
                    ApplyChange((WebsiteDeployedEvent)x.Event);
                });
                
            }
            else if (message is AddAppSettingCommand)
            {
                _sender = Sender;
                var m = (AddAppSettingCommand)message;
                ApplyChange(new AppSettingAddedEvent(aggregateId, m.Key, m.Value));
            }
            else if (message is AppSettingAddedEvent)
            {
                var evt = (AppSettingAddedEvent)message;
                Persist<EventRecord>(new EventRecord(Guid.NewGuid(), 1, aggregateId, evt), x =>
                {
                    ApplyChange((AppSettingAddedEvent)x.Event);
                });
            }

            return true;
        }

        private void ApplyChange(WebsiteDeployedEvent evt)
        {
            Console.WriteLine(
                $"Project is deployed, state has been applied to AggregateRoot Project, current version is {evt.Version}");
            _sender.Tell(evt);
        }

        private void ApplyChange(AppSettingAddedEvent evt)
        {
            Console.WriteLine(
                $"New setting is added, Key: {evt.Key} Value: {evt.Value}");
            _sender.Tell(evt);
        }

        private void Apply(WebsiteDeployedEvent evt)
        {
            _currentVersion = evt.Version;
            Console.WriteLine($"AggregateRoot Project is being reloaded, applying version number {evt.Version}");
        }

        private void Apply(AppSettingAddedEvent evt)
        {
            // Nothing need to be done now
        }
        

        public override string PersistenceId { get; }
        
    }
}
