using System;
using Akka.Actor;
using OctoPoC.Messages.Commands;
using OctoPoC.Messages.Events;
using System.Timers;

namespace OctoPoC.Core.DeploymentTargets
{
    public class CloudRegionActor : ReceiveActor
    {
        public CloudRegionActor()
        {
            var targetId = Guid.NewGuid();
            Receive<StartHeartbeatCommand>(cmd =>
            {
                
               Sender.Tell(new TargetPulseEvent(targetId, DateTimeOffset.Now, "Cloud Region"));
               
            });
        }
    }
}
