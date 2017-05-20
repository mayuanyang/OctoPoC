using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OctoPoC.Core.EventStore
{
    public interface IEventStore
    {
        Task<IEnumerable<EventRecord>> Load(Guid aggregateId);
        Task Save(EventRecord record);
    }
}
