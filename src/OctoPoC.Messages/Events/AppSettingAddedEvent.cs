using System;
using OctoPoC.Messages.MessageContracts;

namespace OctoPoC.Messages.Events
{
    public class AppSettingAddedEvent : IEvent
    {
        public Guid ProjectId { get; }
        public string Key { get; }
        public string Value { get; }

        public AppSettingAddedEvent(Guid projectId, string key, string value)
        {
            ProjectId = projectId;
            Key = key;
            Value = value;
        }
    }
}
