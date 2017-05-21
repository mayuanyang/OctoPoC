using System;
using System.Collections.Generic;

namespace OctoPoC.Core.Environments
{
    class InMemoryEnvironmentRepo : IEnvironmentRepo
    {
        private IEnumerable<DeployEnvironment> _environments;
        public InMemoryEnvironmentRepo()
        {
            _environments = new []
            {
                new DeployEnvironment(Guid.NewGuid(), "CI" ),
                new DeployEnvironment(Guid.NewGuid(), "PreProd" ),
                new DeployEnvironment(Guid.NewGuid(), "Prod" )
            };
        }
        public IEnumerable<DeployEnvironment> LoadAll()
        {
            return _environments;
        }
    }
}