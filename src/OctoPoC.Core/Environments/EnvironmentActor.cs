using System;
using Akka.Actor;
using Akka.DI.Core;
using OctoPoC.Core.Environments.DeploymentTargets;
using OctoPoC.Messages.Commands;

namespace OctoPoC.Core.Environments
{
    public class EnvironmentActor : ReceiveActor
    {
        public EnvironmentActor(IEnvironmentRepo environmentRepo)
        {
            Receive<CheckEnvironmentCommand>(cmd =>
            {
                Console.WriteLine("Start sending ReportHeartbeatCommand to all deployment targets");

                var listeningTentacleActorProps = Context.DI().Props<ListeningTentacleActor>();
                var listeningTentacleActor = Context.ActorOf(listeningTentacleActorProps, "ListeningTentacle");

                var couldRegioinActorProps = Context.DI().Props<CloudRegionActor>();
                var couldRegioinActor = Context.ActorOf(couldRegioinActorProps, "CloudRegionActor");
                
                listeningTentacleActor.Tell(new ReportHeartbeatCommand(), Sender);
                couldRegioinActor.Tell(new ReportHeartbeatCommand(), Sender);
            });
            
        }
    }
}
