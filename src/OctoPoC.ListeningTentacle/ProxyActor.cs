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
            
            var tentacleActor = Context.ActorOf(Context.DI().Props<ListeningTentacleActor>(), "ListeningTentacle");
            
            Receive<string>(x =>
            {
                Console.WriteLine($"Receive message: {x}");
            });

            Receive<ReportHeartbeatCommand>(x =>
            {
                tentacleActor.Tell(x, Sender);
            });

            Receive<DeployWebsiteCommand>(x =>
            {
                var project = Context.ActorOf(Context.DI().Props<ProjectActor>(), $"project-{Guid.NewGuid()}");
                project.Tell(x, Sender);
            });

            Receive<AddAppSettingCommand>(x =>
            {
                var project = Context.ActorOf(Context.DI().Props<ProjectActor>(), $"project-{Guid.NewGuid()}");
                project.Tell(x, Sender);
            
            });
        }
    }
}
