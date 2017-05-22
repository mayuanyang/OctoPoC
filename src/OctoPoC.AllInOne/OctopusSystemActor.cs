using System;
using Akka.Actor;
using Akka.DI.Core;
using OctoPoC.Core.Environments;
using OctoPoC.Messages.Commands;
using OctoPoC.Messages.Events;

namespace OctoPoC.AllInOne
{
    public class OctopusSystemActor : TypedActor, IHandle<StartSystemCommand>, IHandle<TargetPulsedEvent>
    {
        public void Handle(StartSystemCommand message)
        {
            Console.WriteLine("StartSystemCommand received");
            Sender.Tell(new ServerStartedEvent());

            var envActorProps = Context.DI().Props<EnvironmentActor>();
            var envActor = Context.ActorOf(envActorProps, "EnvironmentActor");
            envActor.Tell(new CheckEnvironmentCommand());
        }

        public void Handle(TargetPulsedEvent x)
        {
            
            Console.WriteLine($"Heartbeat: [{x.TargetType}] Id: {x.TargetId} at {x.DateTimeOffset}");
            
        }
    }
}
