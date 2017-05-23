using System;

namespace OctoPoC.Messages.RequestResponses
{
    public class ProjectAppSettingDto
    {
        public Guid ProjectId { get; }
        public string Key { get; }
        public string Value { get; internal set; }
        public string Version { get; internal set; }
        public DateTimeOffset RecordTime { get; internal set; }
        public string Operation { get; internal set; }

        public ProjectAppSettingDto(Guid projectId, string key, string value, string version, DateTimeOffset recordTime, string operation)
        {
            ProjectId = projectId;
            Key = key;
            Value = value;
            Version = version;
            RecordTime = recordTime;
            Operation = operation;
        }
    }
}
