using System;
using OctoPoC.Messages.MessageContracts;

namespace OctoPoC.Messages.Commands
{
    public class CreateReleaseForProjectCommand : ICommand
    {
        public Guid ProjectId { get; }
        public string VersionNumber { get; }
        public string NugetPackagePath { get; }

        public CreateReleaseForProjectCommand(Guid projectId, string versionNumber, string nugetPackagePath)
        {
            ProjectId = projectId;
            VersionNumber = versionNumber;
            NugetPackagePath = nugetPackagePath;
        }
    }
}
