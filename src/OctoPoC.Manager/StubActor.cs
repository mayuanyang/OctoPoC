using System;
using Akka.Actor;
using OctoPoC.Messages.Commands;
using OctoPoC.Messages.Events;

namespace OctoPoC.Manager
{
    class StubActor : TypedActor, IHandle<ConnectToTargetsCommand>, IHandle<TargetPulsedEvent>
    {
        private readonly ActorSelection _cloudServer;
        private readonly ActorSelection _listeningTentacle;
        public StubActor()
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
    }
}
