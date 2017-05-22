﻿using Akka.Persistence;
using OctoPoC.Core.EventStore;

namespace OctoPoC.Core.Settings
{
    public class AppSettingActor : ReceivePersistentActor
    {
        private readonly IEventStore _eventStore;
        public override string PersistenceId { get; }

        public AppSettingActor(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

    }

    
}