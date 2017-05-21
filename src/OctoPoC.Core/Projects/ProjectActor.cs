using System;
using Akka.Persistence;
using OctoPoC.Core.EventStore;

namespace OctoPoC.Core.Projects
{
    public class ProjectActor : PersistentActor
    {
        private readonly IEventStore _eventStore;

        public ProjectActor(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }
    
        protected override bool ReceiveRecover(object message)
        {
            Console.WriteLine(nameof(ReceiveRecover));
            return true;
        }

        protected override bool ReceiveCommand(object message)
        {
            Console.WriteLine(nameof(ReceiveCommand));
            return true;
        }

        public override string PersistenceId { get; }
    }
}
