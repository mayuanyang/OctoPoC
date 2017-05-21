using System;
using OctoPoC.Messages.MessageContracts;

namespace OctoPoC.Messages.Commands
{
    public class DeployProjectCommand : ICommand
    {
        public Guid ProjectId { get; }
        public string DeployToEnvironment { get; }

        public DeployProjectCommand(Guid projectId, string deployToEnvironment)
        {
            ProjectId = projectId;
            DeployToEnvironment = deployToEnvironment;
        }
    }
}
