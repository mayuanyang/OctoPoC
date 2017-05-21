using System.Collections.Generic;
using System.Security.Policy;

namespace OctoPoC.Core.Environments.DeploymentTargets
{
    public class InMemoryTargetLoader : ITargetLoader
    {
        private IEnumerable<DeploymentTarget> _deploymentTargets;
        public InMemoryTargetLoader()
        {
            _deploymentTargets = new[]
            {
                new DeploymentTarget(TargetStyle.ListeningTentacle, new Url(@"http://localhost:10944"), new[] {"CI"}),
                new DeploymentTarget(TargetStyle.CloudRegion, new Url(@"http://localhost:10945"), new[] {"CI"})
            };
        }
        public IEnumerable<DeploymentTarget> LoadAllTargets()
        {
            return _deploymentTargets;
        }
    }
}