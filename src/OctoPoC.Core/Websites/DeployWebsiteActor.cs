using System;
using Akka.Actor;
using OctoPoC.Messages.Commands;
using OctoPoC.Messages.Events;

namespace OctoPoC.Core.Websites
{
    public class DeployWebsiteActor : TypedActor, IHandle<DeployWebsiteCommand>
    {
        public void Handle(DeployWebsiteCommand message)
        {
            Console.WriteLine($"Trying to deploy website {message.WebsiteName}");
            Sender.Tell(new WebsiteDeployedEvent(message.WebsiteName, message.Version));
        }
    }
}
