using Akka.Actor;
using OctoPoC.Core.Servers;
using OctoPoC.Messages.Commands;

namespace OctoPoC.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var actorSystem = ActorSystem.Create("OctopusSystem"))
            {
                var server = actorSystem.ActorOf(Props.Create(() => new OctopusServerActor()), "OctopusServer");
                server.Tell(new StartServerCommand(), server);
                
                actorSystem.WhenTerminated.Wait();
            }
        }
    }
}
