using System;
using OctoPoC.Messages.MessageContracts;

namespace OctoPoC.Messages.Events
{
    public class TargetPulsedEvent : IEvent
    {
        public Guid TargetId { get; }
        public DateTimeOffset DateTimeOffset { get; }
        public string TargetType { get; }


        public TargetPulsedEvent(Guid targetId, DateTimeOffset dateTimeOffset, string targetType)
        {
            TargetId = targetId;
            DateTimeOffset = dateTimeOffset;
            TargetType = targetType;
        }
    }
}
