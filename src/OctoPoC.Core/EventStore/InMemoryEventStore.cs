using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OctoPoC.Core.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        private List<EventRecord> _eventRecords;
        public InMemoryEventStore()
        {
            _eventRecords = new List<EventRecord>();
        }
        public Task<IEnumerable<EventRecord>> Load(Guid aggregateId)
        {
            var events = _eventRecords.Where(x => x.AggregateId == aggregateId).OrderBy(x => x.Sequence).AsEnumerable();
            return Task.FromResult(events);
        }

        public Task Save(EventRecord record)
        {
            _eventRecords.Add(record);
            return Task.FromResult(0);
        }
    }
}