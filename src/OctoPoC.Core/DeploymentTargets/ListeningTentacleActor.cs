using System;
using Akka.Actor;
using OctoPoC.Messages.Commands;
using OctoPoC.Messages.Events;

namespace OctoPoC.Core.DeploymentTargets
{
    public class ListeningTentacleActor : ReceiveActor
    {
        public ListeningTentacleActor()
        {
            var targetId = Guid.NewGuid();
            Receive<StartHeartbeatCommand>(cmd =>
            {
                Sender.Tell(new TargetPulseEvent(targetId, DateTimeOffset.Now, "Listening Tentacle"));
               
            });
        }
    }
}
