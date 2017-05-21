using System;

namespace OctoPoC.Core.Environments
{
    public class DeployEnvironment
    {
        public Guid Id { get; }
        public string Name { get; }

        public DeployEnvironment(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}