using System.Security.Policy;

namespace OctoPoC.Core.Environments.DeploymentTargets
{
    public class DeploymentTarget
    {
        public TargetStyle TargetStyle { get; }
        public Url Url { get; }
        public string[] Roles { get; }

        public DeploymentTarget(TargetStyle targetStyle, Url url, string[] roles)
        {
            TargetStyle = targetStyle;
            Url = url;
            Roles = roles;
        }
    }
}