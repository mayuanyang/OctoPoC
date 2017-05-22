using System;
using Akka.Actor;
using Akka.DI.Core;
using OctoPoC.Core.Environments.DeploymentTargets;
using OctoPoC.Messages.Commands;

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
        }
    }
}
