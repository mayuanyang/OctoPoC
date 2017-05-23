using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Akka.Actor;
using OctoPoC.Messages.RequestResponses;

namespace OctoPoC.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OctopusManagerController : ApiController
    {
        private readonly ActorSystem _actorSystem;
        private readonly ActorSelection _stub;
        
        public OctopusManagerController(ActorSystem actorSystem)
        {
            _actorSystem = actorSystem;
            var path = "/user/stub";
            _stub = _actorSystem.ActorSelection(path);
        }
        [HttpPost]
        public Task ConnectAllTargets()
        {
            return Task.FromResult(1);
        }

        [HttpGet]
        public async Task<GetAllAppSettingsResponse> GetAllSettings()
        {
            
            var response = await _stub.Ask<GetAllAppSettingsResponse>(new GetAllAppSettingsRequest(Guid.Parse("30a37555-780a-4748-962c-26a4d163f56c")));
            return response;
        }
    }
}
