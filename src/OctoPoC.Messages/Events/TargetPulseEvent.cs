using System;
using OctoPoC.Messages.MessageContracts;

namespace OctoPoC.Messages.Events
{
    public class TargetPulseEvent : IEvent
    {
        public Guid TargetId { get; }
        public DateTimeOffset DateTimeOffset { get; }
        public string TargetType { get; }


        public TargetPulseEvent(Guid targetId, DateTimeOffset dateTimeOffset, string targetType)
        {
            TargetId = targetId;
            DateTimeOffset = dateTimeOffset;
            TargetType = targetType;
        }
    }
}
