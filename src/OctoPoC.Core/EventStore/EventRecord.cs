using System;
using OctoPoC.Messages.MessageContracts;

namespace OctoPoC.Core.EventStore
{
    public class EventRecord
    {
        public Guid Id { get; }
        public long Sequence { get; }
        public Guid AggregateId { get; }
        public IEvent Event { get; }

        public EventRecord(Guid id, long sequence, Guid aggregateId, IEvent @event)
        {
            Id = id;
            Sequence = sequence;
            AggregateId = aggregateId;
            Event = @event;
        }
    }

    
}
