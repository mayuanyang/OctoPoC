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
            Console.WriteLine("3: Add a setting");
            Console.WriteLine("4: Update a setting");
            var option = Console.ReadLine();
            int version = 1;
            
            using (var system = ActorSystem.Create("OctopusManager", config))
            {
                var stub = system.ActorOf<StubActor>($"stub");
                while (true)
                {
                    if (option == "1")
                    {
                        stub.Tell(new ConnectToTargetsCommand());
                    }
                    else if (option == "2")
                    {
                        stub.Tell(new DeployWebsiteCommand("helloworld", "helloworld", "9876", $"{version}.0.0", null), stub);
                        version += 1;
                    }
                    else if (option == "3")
                    {
                        Console.WriteLine("Enter the key");
                        var key = Console.ReadLine();
                        Console.WriteLine("Enter the value");
                        var value = Console.ReadLine();
                        stub.Tell(new AddAppSettingCommand(Guid.NewGuid(), key, value, "1", DateTimeOffset.Now));
                    }
                    else if (option == "4")
                    {
                        Console.WriteLine("Enter the key");
                        var key = Console.ReadLine();
                        Console.WriteLine("Enter the value");
                        var value = Console.ReadLine();
                        stub.Tell(new UpdateAppSettingCommand(Guid.NewGuid(), key, value, "1", DateTimeOffset.Now));
                    }
                    option = Console.ReadLine();
                }
                
            }
        }
    }
}
