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
        

        public ProjectActor()
        {
            PersistenceId = "30a37555-780a-4748-962c-26a4d163f56c";
        }
    
        protected override bool ReceiveRecover(object message)
        {
            if (message is EventRecord)
            {
                var evt = (EventRecord)message;
                var websiteEvt = (WebsiteDeployedEvent) evt.Event;
                Apply(websiteEvt, true);
            }
            return true;
        }

        protected override bool ReceiveCommand(object message)
        {
            if (message is DeployWebsiteCommand)
            {
                _sender = Sender;
                var websiteActorProps = Context.DI().Props<DeployWebsiteActor>();
                var websiteActor = Context.ActorOf(websiteActorProps, "WebsiteActor");
                websiteActor.Tell(message, Self);
            }
            else if (message is WebsiteDeployedEvent)
            {
                var aggregateId = Guid.Parse(PersistenceId);
                var evt = (WebsiteDeployedEvent) message;
                Persist<EventRecord>(new EventRecord(Guid.NewGuid(), 1, aggregateId, evt), x =>
                {
                    Apply(evt, false);
                });
                
            }
            return true;
        }

        private void Apply(WebsiteDeployedEvent evt, bool isFromRecover)
        {
            _currentVersion = evt.Version;

            if (!isFromRecover)
            {
                Console.WriteLine(
                    $"Project is deployed, state has been applied to AggregateRoot Project, current version is {evt.Version}");
                _sender.Tell(evt);
            }
            else
            {
                Console.WriteLine($"AggregateRoot Project is being reloaded, applying version number {evt.Version}");
            }
           
        }
        

        public override string PersistenceId { get; }
        
    }
}
