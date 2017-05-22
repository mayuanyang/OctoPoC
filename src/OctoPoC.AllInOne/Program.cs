using System;
using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Autofac;
using OctoPoC.Core;
using OctoPoC.Core.PowerShell;
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
           
            Console.WriteLine("1: Check targets");
            Console.WriteLine("2: Deploy a Demo website");
            var option = Console.ReadLine();
            using (var actorSystem = ActorSystem.Create("OctopusManager"))
            {
                var propsResolver = new AutoFacDependencyResolver(container, actorSystem);
                if (option == "1")
                {
                    var server = actorSystem.ActorOf(actorSystem.DI().Props<OctopusSystemActor>(), "OctopusServer");
                    server.Tell(new StartSystemCommand(), server);
                }
                else if (option == "2")
                {
                    var project = actorSystem.ActorOf(actorSystem.DI().Props<ProjectActor>(), "project");
                    project.Tell(new DeployWebsiteCommand("helloworld", "helloworld", "9876", null), project);

                }
                
                actorSystem.WhenTerminated.Wait();
            }
        }
    }
}
