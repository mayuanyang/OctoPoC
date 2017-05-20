using System;
using System.Runtime.Remoting.Contexts;
using Akka.Actor;
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
                var listeningTentacleActor = Context.ActorOf<ListeningTentacleActor>("ListeningTentacleActor");
                var couldRegioinActor = Context.ActorOf<CloudRegionActor>("CloudRegionActor");

                Console.WriteLine("Start sending StartHeartbeatCommand to all deployment targets");

                
                listeningTentacleActor.Tell(new StartHeartbeatCommand(), Self);
                couldRegioinActor.Tell(new StartHeartbeatCommand(), Self);

            });

            Receive<TargetPulseEvent>(x =>
            {
                Console.WriteLine($"Received heartbeat from target type [{x.TargetType}] Id: {x.TargetId}");
            });

        }
    }
}
