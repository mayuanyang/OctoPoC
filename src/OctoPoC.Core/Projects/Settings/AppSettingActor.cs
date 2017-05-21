using Akka.Persistence;
using OctoPoC.Core.EventStore;

namespace OctoPoC.Core.Projects.Settings
{
    public class AppSettingActor : ReceivePersistentActor
    {
        private readonly IEventStore _eventStore;
        public override string PersistenceId { get; }

        public AppSettingActor(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        protected override bool Receive(object message)
        {
            return base.Receive(message);
        }
    }

    
}
