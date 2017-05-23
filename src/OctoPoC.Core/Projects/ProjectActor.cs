using System;
using Akka.Actor;
using Akka.DI.Core;
using Akka.Persistence;
using OctoPoC.Core.EventStore;
using OctoPoC.Core.ReadmodelGeneration;
using OctoPoC.Core.Settings;
using OctoPoC.Core.Websites;
using OctoPoC.Messages.Commands;
using OctoPoC.Messages.Events;

namespace OctoPoC.Core.Projects
{
    public class ProjectActor : PersistentActor
    {
        private IActorRef _sender;
        private string _currentVersion = "";
        private IActorRef _websiteDeployActor;
        private IActorRef _appSettingAuditableActor;
        private IActorRef _appSettingNonAuditableActor;
        private IActorRef _settingActor;
        

        public ProjectActor()
        {
            PersistenceId = "30a37555-780a-4748-962c-26a4d163f56c";
            _websiteDeployActor = Context.ActorOf(Context.DI().Props<DeployWebsiteActor>(), "website-actor");
            _appSettingAuditableActor = Context.ActorOf(Context.DI().Props<AppSettingAuditableActor>(), "appsetting-auditable-readmodel");
            _appSettingNonAuditableActor = Context.ActorOf(Context.DI().Props<AppSettingNonAuditableActor>(), "appsetting-nonauditable-readmodel");
            _settingActor = Context.ActorOf(Context.DI().Props<SettingActor>(), "setting-actor");
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
                _websiteDeployActor.Tell(message, Self);
            }
            else if (message is WebsiteDeployedEvent)
            {
                var evt = (WebsiteDeployedEvent) message;
                Persist<EventRecord>(new EventRecord(Guid.NewGuid(), 1, aggregateId, evt, DateTimeOffset.Now), x =>
                {
                    ApplyChange((WebsiteDeployedEvent)x.Event);
                });
            }
            else if (message is AddAppSettingCommand)
            {
                _sender = Sender;
                var m = (AddAppSettingCommand)message;
                _settingActor.Tell(m);
            }
            else if (message is AppSettingAddedEvent)
            {
                var evt = (AppSettingAddedEvent)message;
                Persist<EventRecord>(new EventRecord(Guid.NewGuid(), 1, aggregateId, evt, DateTimeOffset.Now), x =>
                {
                    ApplyChange((AppSettingAddedEvent)x.Event);
                });
            }
            else if (message is UpdateAppSettingCommand)
            {
                _sender = Sender;
                var m = (UpdateAppSettingCommand) message;
                _settingActor.Tell(m);
                
            }
            else if (message is AppSettingUpdatedEvent)
            {
                var evt = (AppSettingUpdatedEvent)message;
                Persist<EventRecord>(new EventRecord(Guid.NewGuid(), 1, aggregateId, evt, DateTimeOffset.Now), x =>
                {
                    ApplyChange((AppSettingUpdatedEvent)x.Event);
                });
            }

            return true;
        }

        private void ApplyChange(WebsiteDeployedEvent evt)
        {
            Console.WriteLine(
                $"Project is deployed, state has been applied to AggregateRoot Project, current version is {evt.Version}");
            _currentVersion = evt.Version;
            _sender.Tell(evt);
        }

        private void ApplyChange(AppSettingAddedEvent evt)
        {
            Console.WriteLine(
                $"New setting is added, Key: {evt.Key} Value: {evt.Value}");
            _sender.Tell(evt);
            _appSettingAuditableActor.Tell(evt);
            _appSettingNonAuditableActor.Tell(evt);
        }

        private void ApplyChange(AppSettingUpdatedEvent evt)
        {
            Console.WriteLine(
                $"New setting is added, Key: {evt.Key} Value: {evt.Value}");
            _sender.Tell(evt);
            _appSettingAuditableActor.Tell(evt);
            _appSettingNonAuditableActor.Tell(evt);
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

        private void Apply(AppSettingUpdatedEvent evt)
        {
            // Nothing need to be done now
        }


        public override string PersistenceId { get; }
        
    }
}
