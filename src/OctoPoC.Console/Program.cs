using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Autofac;
using OctoPoC.Core;
using OctoPoC.Core.Projects;
using OctoPoC.Messages.Commands;

namespace OctoPoC.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new Autofac.ContainerBuilder();
            builder.RegisterModule<AutofacModule>(); 
            var container = builder.Build();
           

            using (var actorSystem = ActorSystem.Create("OctopusManager"))
            {
                var propsResolver = new AutoFacDependencyResolver(container, actorSystem);
                var server = actorSystem.ActorOf(actorSystem.DI().Props<OctopusSystemActor>(), "OctopusServer");
                server.Tell(new StartSystemCommand(), server);

                var projectActor = actorSystem.ActorOf(actorSystem.DI().Props<ProjectActor>(), "project");
                
                actorSystem.WhenTerminated.Wait();
            }
        }
    }
}
