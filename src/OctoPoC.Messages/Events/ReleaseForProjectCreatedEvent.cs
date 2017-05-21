using System;
using OctoPoC.Messages.MessageContracts;

namespace OctoPoC.Messages.Events
{
    public class ReleaseForProjectCreatedEvent : IEvent
    {
        public Guid ProjectId { get; }
        public string VersionNumber { get; }
        public string NugetPackagePath { get; }
        public ReleaseForProjectCreatedEvent(Guid projectId, string versionNumber, string nugetPackagePath)
        {
            ProjectId = projectId;
            VersionNumber = versionNumber;
            NugetPackagePath = nugetPackagePath;
        }
    }
}
