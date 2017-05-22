using Akka.Actor;
using Akka.DI.Core;
using OctoPoC.Core.Environments;
using OctoPoC.Messages.Commands;
using OctoPoC.Messages.Events;

namespace OctoPoC.Console
{
    public class OctopusSystemActor : TypedActor, IHandle<StartSystemCommand>
    {
        public void Handle(StartSystemCommand message)
        {
            System.Console.WriteLine("StartSystemCommand received");
            Sender.Tell(new ServerStartedEvent());

            var envActorProps = Context.DI().Props<EnvironmentActor>();
            var envActor = Context.ActorOf(envActorProps, "EnvironmentActor");
            envActor.Tell(new CheckEnvironmentCommand());
        }
    }
}
