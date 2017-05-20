using System;

namespace OctoPoC.Core.EventStore
{
    public class EventRecord
    {
        public Guid Id { get; }
        public long Sequence { get; }
        public Guid AggregateId { get; }
        public IDomainEvent Event { get; }

        public EventRecord(Guid id, long sequence, Guid aggregateId, IDomainEvent @event)
        {
            Id = id;
            Sequence = sequence;
            AggregateId = aggregateId;
            Event = @event;
        }
    }

    
}
