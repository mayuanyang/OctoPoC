using System.Collections.Generic;

namespace OctoPoC.Core.DeploymentTargets
{
    public interface ITargetLoader
    {
        IEnumerable<DeploymentTarget> LoadAllTargets();
    }
}
