using System;
using Akka.Actor;
using OctoPoC.Messages.Commands;
using OctoPoC.Messages.Events;
using OctoPoC.Messages.RequestResponses;

namespace OctoPoC.WebApi
{
    class StubActor : TypedActor, 
        IHandle<TargetPulsedEvent>, 
        IHandle<DeployWebsiteCommand>, 
        IHandle<WebsiteDeployedEvent>,
        IHandle<AddAppSettingCommand>,
        IHandle<AppSettingAddedEvent>,
        IHandle<GetAllAppSettingsRequest>
    {
        private readonly ActorSelection _cloudServer;
        private readonly ActorSelection _listeningTentacle;
        public StubActor()
        {
             _cloudServer = Context.ActorSelection("akka.tcp://OctopusCloudRegionTarget@localhost:9082/user/Proxy");
            _listeningTentacle = Context.ActorSelection("akka.tcp://OctopusListeningTentacleTarget@localhost:9081/user/Proxy");
        }
        
        public void Handle(TargetPulsedEvent x)
        {
            Console.WriteLine($"Heartbeat: [{x.TargetType}] Id: {x.TargetId} at {x.DateTimeOffset}");
        }

        public void Handle(DeployWebsiteCommand message)
        {
            _listeningTentacle.Tell(message);
        }

        public void Handle(WebsiteDeployedEvent message)
        {
            Console.WriteLine($"Website {message.WebsiteName} has been successfully deployed, current version is {message.Version}");
        }

        public void Handle(AddAppSettingCommand message)
        {
            _listeningTentacle.Tell(message);
        }

        public void Handle(UpdateAppSettingCommand message)
        {
            _listeningTentacle.Tell(message);
        }

        public void Handle(AppSettingAddedEvent message)
        {
            Console.WriteLine($"AppSetting Key: {message.Key} Value: {message.Value} has been successfully added");
        }

        public void Handle(AppSettingUpdatedEvent message)
        {
            Console.WriteLine($"AppSetting Key: {message.Key} Value: {message.Value} has been successfully updated");
        }

        public void Handle(GetAllAppSettingsRequest message)
        {
            _listeningTentacle.Tell(message, Sender);
        }
    }
}
