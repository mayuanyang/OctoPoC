using System;
using Akka.Actor;
using Akka.DI.Core;
using OctoPoC.Core.Environments;
using OctoPoC.Messages.Commands;
using OctoPoC.Messages.Events;

namespace OctoPoC.Core
{
    public class OctopusSystemActor : ReceiveActor
    {
        public OctopusSystemActor()
        {
            Receive<StartSystemCommand>(x =>
            {
                Console.WriteLine("StartSystemCommand received");
                Sender.Tell(new ServerStartedEvent());

                var envActorProps = Context.DI().Props<EnvironmentActor>();
                var envActor = Context.ActorOf(envActorProps, "EnvironmentActor");
                envActor.Tell(new CheckEnvironmentCommand());

            });
        }
    }
}
