using System;
using System.Threading.Tasks;
using Akka.Actor;
using OctoPoC.Messages.Commands;
using OctoPoC.Messages.Events;

namespace OctoPoC.Core.Environments.DeploymentTargets
{
    public class ListeningTentacleActor : ReceiveActor
    {
        public ListeningTentacleActor()
        {
            var targetId = Guid.NewGuid();
            Receive<ReportHeartbeatCommand>(cmd =>
            {
                while (true)
                {
                    Sender.Tell(new TargetPulsedEvent(targetId, DateTimeOffset.Now, "Listening Tentacle"));
                    Task.Delay(10000).Wait();
                }

            });
        }
    }
}
