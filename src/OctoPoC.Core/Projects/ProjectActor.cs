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
        private readonly IEventStore _eventStore;

        public ProjectActor(IEventStore eventStore)
        {
            _eventStore = eventStore;
            PersistenceId = "30a37555-780a-4748-962c-26a4d163f56c";
        }
    
        protected override bool ReceiveRecover(object message)
        {
            Console.WriteLine(message.GetType().Name);
            return true;
        }

        protected override bool ReceiveCommand(object message)
        {
            if (message is DeployWebsiteCommand)
            {
                var websiteActorProps = Context.DI().Props<DeployWebsiteActor>();
                var websiteActor = Context.ActorOf(websiteActorProps, "WebsiteActor");
                websiteActor.Tell(message, Sender);
            } 
            return true;
        }
        

        public override string PersistenceId { get; }
        
    }
}
