using System;
using Akka.Actor;
using Akka.Configuration;

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

            using (var system = ActorSystem.Create("OctopusManager", config))
            {
                var stub = system.ActorOf<StubActor>("stub");
                stub.Tell(new ConnectToTargetsCommand());

                Console.ReadLine();
            }
        }
    }
}
