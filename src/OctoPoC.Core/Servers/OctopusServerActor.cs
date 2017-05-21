using System;
using System.Runtime.Remoting.Contexts;
using Akka.Actor;
using Akka.DI.Core;
using OctoPoC.Core.DeploymentTargets;
using OctoPoC.Messages.Commands;
using OctoPoC.Messages.Events;

namespace OctoPoC.Core.Servers
{
    public class OctopusServerActor : ReceiveActor
    {
        public OctopusServerActor()
        {
            Receive<StartServerCommand>(x =>
            {
                Console.WriteLine("StartServerCommand received");
                Sender.Tell(new ServerStartedEvent());

                var listeningTentacleActorProps = Context.DI().Props<ListeningTentacleActor>();
                var listeningTentacleActor = Context.ActorOf(listeningTentacleActorProps, "ListeningTentacle");

                var couldRegioinActorProps = Context.DI().Props<CloudRegionActor>();
                var couldRegioinActor = Context.ActorOf(couldRegioinActorProps, "CloudRegionActor");

                Console.WriteLine("Start sending StartHeartbeatCommand to all deployment targets");
                
                listeningTentacleActor.Tell(new StartHeartbeatCommand(), Self);
                couldRegioinActor.Tell(new StartHeartbeatCommand(), Self);

            });

            Receive<TargetPulseEvent>(x =>
            {
                Console.WriteLine($"Received heartbeat from target type [{x.TargetType}] Id: {x.TargetId} at {x.DateTimeOffset}");
            });

        }
    }
}
