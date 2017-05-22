using System.Threading.Tasks;
using System.Web.Http;
using Akka.Actor;
using OctoPoC.Core.DeploymentTargets;
using OctoPoC.Messages.Commands;

namespace OctoPoC.WebApi.Controllers
{
    public class OctopusManagerController : ApiController
    {
        private readonly ActorSystem _actorSystem;

        public OctopusManagerController(ActorSystem actorSystem)
        {
            _actorSystem = actorSystem;
        }
        [HttpPost]
        public Task ConnectAllTargets()
        {
            return Task.FromResult(1);
        }

        [HttpGet]
        public Task<string> Get()
        {
            var cloudActor = _actorSystem.ActorOf<CloudRegionActor>();
            cloudActor.Tell(new ReportHeartbeatCommand());
            return Task.FromResult("hello world");
        }
    }
}
