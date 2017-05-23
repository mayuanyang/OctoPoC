using System;
using System.Web.Http;
using Akka.Actor;
using Akka.Configuration;
using Akka.DI.AutoFac;
using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json.Serialization;
using OctoPoC.Core;
using OctoPoC.Messages.Commands;

namespace OctoPoC.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            HttpConfiguration httpconfig = GlobalConfiguration.Configuration;
            httpconfig.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            httpconfig.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacModule>();
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);
            

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
            
            var system = ActorSystem.Create("OctoPoCWebApi", config);
            var stub = system.ActorOf<StubActor>("stub");
            builder.RegisterInstance(system).SingleInstance();
            var container = builder.Build();
            
            var propsResolver = new AutoFacDependencyResolver(container, system);
            
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
