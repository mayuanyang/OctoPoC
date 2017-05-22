using System;
using Akka.Actor;
using Akka.Configuration;
using OctoPoC.Core.Projects;
using OctoPoC.Messages.Commands;

namespace OctoPoC.Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(@"
akka {  
    actor {
        provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
    }
    remote {
        dot-netty.tcp {
            port = 0
            hostname = localhost
        }
    }
}
");

            Console.WriteLine("1: Check targets");
            Console.WriteLine("2: Deploy a Demo website");
            var option = Console.ReadLine();
            

            using (var system = ActorSystem.Create("OctopusManager", config))
            {
                while (true)
                {
                    if (option == "1")
                    {
                        var stub = system.ActorOf<StubActor>("stub-heartbeat");
                        stub.Tell(new ConnectToTargetsCommand());
                    }
                    else if (option == "2")
                    {
                        var stub = system.ActorOf<StubActor>("stub-deploy");
                        stub.Tell(new DeployWebsiteCommand("helloworld", "helloworld", "9876", null), stub);

                    }

                    option = Console.ReadLine();
                }
                
            }
        }
    }
}
