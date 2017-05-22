using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Autofac;
using OctoPoC.Core;
using OctoPoC.Core.Projects;
using OctoPoC.Messages.Commands;

namespace OctoPoC.AllInOne
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new Autofac.ContainerBuilder();
            builder.RegisterModule<AutofacModule>();
            builder.RegisterType<OctopusSystemActor>();
            var container = builder.Build();
           

            using (var actorSystem = ActorSystem.Create("OctopusManager"))
            {
                var propsResolver = new AutoFacDependencyResolver(container, actorSystem);
                var server = actorSystem.ActorOf(actorSystem.DI().Props<OctopusSystemActor>(), "OctopusServer");
                server.Tell(new StartSystemCommand(), server);
                
                actorSystem.WhenTerminated.Wait();
            }
        }
    }
}
