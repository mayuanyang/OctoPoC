﻿using System;
using Akka.Actor;
using Akka.DI.Core;
using OctoPoC.Core.Projects;
using OctoPoC.Core.ReadLayer;
using OctoPoC.Messages.Commands;
using OctoPoC.Messages.Events;
using OctoPoC.Messages.RequestResponses;

namespace OctoPoC.Manager
{
    class OctoPoCServerEndpointActor : TypedActor, 
        IHandle<ConnectToTargetsCommand>, 
        IHandle<TargetPulsedEvent>, 
        IHandle<DeployWebsiteCommand>, 
        IHandle<WebsiteDeployedEvent>,
        IHandle<AddAppSettingCommand>,
        IHandle<AppSettingAddedEvent>,
        IHandle<GetAllAppSettingsRequest>
    {
        private readonly ActorSelection _cloudServer;
        private readonly ActorSelection _listeningTentacle;
        public OctoPoCServerEndpointActor()
        {
             _cloudServer = Context.ActorSelection("akka.tcp://OctopusCloudRegionTarget@localhost:9082/user/Proxy");
            _listeningTentacle = Context.ActorSelection("akka.tcp://OctopusListeningTentacleTarget@localhost:9081/user/Proxy");
        }


        public void Handle(ConnectToTargetsCommand message)
        {
            _cloudServer.Tell(new ReportHeartbeatCommand());
            _listeningTentacle.Tell(new ReportHeartbeatCommand());
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
            var project = Context.ActorOf(Context.DI().Props<ProjectActor>(), $"project-{Guid.NewGuid()}");
            project.Tell(message, Self);
        }

        public void Handle(UpdateAppSettingCommand message)
        {
            var project = Context.ActorOf(Context.DI().Props<ProjectActor>(), $"project-{Guid.NewGuid()}");
            project.Tell(message, Self);
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
            var settingQueryActor = Context.ActorOf(Context.DI().Props<AppSettingsReadActor>(),
                $"setting-reader-{Guid.NewGuid()}");
            settingQueryActor.Tell(message, Sender);
        }
    }
}
