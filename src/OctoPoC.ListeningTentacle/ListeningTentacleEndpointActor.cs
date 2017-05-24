using System;
using System.Configuration;
using Akka.Actor;
using Akka.DI.Core;
using OctoPoC.Core.DeploymentTargets;
using OctoPoC.Core.Projects;
using OctoPoC.Core.ReadLayer;
using OctoPoC.Messages.Commands;
using OctoPoC.Messages.RequestResponses;

namespace OctoPoC.ListeningTentacle
{
    class ListeningTentacleEndpointActor : ReceiveActor
    {
        public ListeningTentacleEndpointActor()
        {
            
            var tentacleActor = Context.ActorOf(Context.DI().Props<ListeningTentacleActor>(), "ListeningTentacle");
            
            Receive<string>(x =>
            {
                Console.WriteLine($"Receive message: {x}");
            });

            Receive<ReportHeartbeatCommand>(x =>
            {
                Console.WriteLine($"Received {nameof(ReportHeartbeatCommand)} from server");
                tentacleActor.Tell(x, Sender);
            });

            Receive<DeployWebsiteCommand>(x =>
            {
                Console.WriteLine($"Received {nameof(DeployWebsiteCommand)} from server");
                var project = Context.ActorOf(Context.DI().Props<ProjectActor>(), $"project-{Guid.NewGuid()}");
                project.Tell(x, Sender);
            });

            
        }
    }
}
