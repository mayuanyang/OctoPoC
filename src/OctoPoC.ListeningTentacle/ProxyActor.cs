using System;
using Akka.Actor;
using Akka.DI.Core;
using OctoPoC.Core.DeploymentTargets;
using OctoPoC.Core.Projects;
using OctoPoC.Messages.Commands;
using OctoPoC.Messages.Events;

namespace OctoPoC.ListeningTentacle
{
    class ProxyActor : ReceiveActor
    {
        public ProxyActor()
        {
            Receive<string>(x =>
            {
                Console.WriteLine($"Receive message: {x}");
            });

            Receive<ReportHeartbeatCommand>(x =>
            {
                var tentacleActorProps = Context.DI().Props<ListeningTentacleActor>();
                var tentacleActor = Context.ActorOf(tentacleActorProps, "ListeningTentacle");
                tentacleActor.Tell(x, Sender);
            });

            Receive<DeployWebsiteCommand>(x =>
            {
                var project = Context.ActorOf(Context.DI().Props<ProjectActor>(), $"project-{x.Version}");
                project.Tell(x, Sender);
            });

        }
    }
}
