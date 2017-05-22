using Akka.Actor;
using Akka.Configuration;
using Akka.DI.AutoFac;
using Autofac;
using OctoPoC.Core;

namespace OctoPoC.ListeningTentacle
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
            port = 9081
            hostname = 0.0.0.0
            public-hostname = localhost
        }
    }
}
");

            var builder = new Autofac.ContainerBuilder();
            builder.RegisterModule<AutofacModule>();
            var container = builder.Build();


            using (var actorSystem = ActorSystem.Create("OctopusListeningTentacleTarget", config))
            {
                var propsResolver = new AutoFacDependencyResolver(container, actorSystem);
                actorSystem.ActorOf<ProxyActor>("Proxy");
                actorSystem.WhenTerminated.Wait();
            }
        }
    }
}
