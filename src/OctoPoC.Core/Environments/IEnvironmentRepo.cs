using System.Collections.Generic;

namespace OctoPoC.Core.Environments
{
    public interface IEnvironmentRepo
    {
        IEnumerable<DeployEnvironment> LoadAll();
    }
}
