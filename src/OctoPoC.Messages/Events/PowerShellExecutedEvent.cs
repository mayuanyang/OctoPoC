using System;
using OctoPoC.Messages.MessageContracts;

namespace OctoPoC.Messages.Events
{
    public class PowerShellExecutedEvent : IEvent
    {
        public Guid Id { get; }

        public PowerShellExecutedEvent(Guid id)
        {
            Id = id;
        }
    }
}
