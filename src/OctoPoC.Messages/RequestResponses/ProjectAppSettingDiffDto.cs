using System;

namespace OctoPoC.Messages.RequestResponses
{
    public class ProjectAppSettingDiffDto
    {
        public Guid ProjectId { get; }
        public string Key { get; }
        public string OriginalValue { get; }
        public string NewValue { get; }
        public string Version { get; internal set; }
        public DateTimeOffset RecordTime { get; internal set; }
        public string Operation { get; internal set; }

        public ProjectAppSettingDiffDto(Guid projectId, string key, string originalValue, string newValue, string version, DateTimeOffset recordTime, string operation)
        {
            ProjectId = projectId;
            Key = key;
            OriginalValue = originalValue;
            NewValue = newValue;
            Version = version;
            RecordTime = recordTime;
            Operation = operation;
        }
    }
}
