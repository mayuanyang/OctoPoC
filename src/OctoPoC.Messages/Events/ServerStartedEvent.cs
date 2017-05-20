using System;
using OctoPoC.Messages.MessageContracts;

namespace OctoPoC.Messages.Events
{
    public class ServerStartedEvent : IEvent
    {
        public DateTimeOffset StartedDate { get; }
        public ServerStartedEvent()
        {
            StartedDate = DateTimeOffset.Now;
        }
    }
}
