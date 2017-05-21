using System.Collections.Generic;

namespace OctoPoC.Core.Environments.DeploymentTargets
{
    public interface ITargetLoader
    {
        IEnumerable<DeploymentTarget> LoadAllTargets();
    }
}
