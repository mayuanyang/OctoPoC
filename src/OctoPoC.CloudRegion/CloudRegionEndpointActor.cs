using System;
using Akka.Actor;
using Akka.DI.Core;
using OctoPoC.Core.DeploymentTargets;
using OctoPoC.Messages.Commands;

namespace OctoPoC.CloudRegion
{
    class CloudRegionEndpointActor : ReceiveActor
    {
        public CloudRegionEndpointActor()
        {
            Receive<string>(x =>
            {
                Console.WriteLine($"Receive message: {x}");
            });

            Receive<ReportHeartbeatCommand>(x =>
            {
                var cloudActorProps = Context.DI().Props<CloudRegionActor>();
                var cloudActor = Context.ActorOf(cloudActorProps, "CloudRegion");
                cloudActor.Tell(x, Sender);
            });
        }
    }
}
