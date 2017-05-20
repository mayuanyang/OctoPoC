using System;

namespace OctoPoC.Core.EventStore
{
    public interface IDomainEvent
    {
        Guid AggregateId { get; }
        Guid EntityId { get; }
    }
}
