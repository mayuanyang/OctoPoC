using System;
using OctoPoC.Messages.MessageContracts;

namespace OctoPoC.Messages.Commands
{
    public class UpdateAppSettingCommand : ICommand
    {
        public Guid ProjectId { get; }
        public string Key { get; }
        public string Value { get; }
        public string Version { get; }
        public DateTimeOffset RecordTime { get; }

        public UpdateAppSettingCommand(Guid projectId, string key, string value, string version, DateTimeOffset recordTime)
        {
            ProjectId = projectId;
            Key = key;
            Value = value;
            Version = version;
            RecordTime = recordTime;
        }
    }
}
