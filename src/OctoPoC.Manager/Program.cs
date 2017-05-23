using System;
using Akka.Actor;
using Akka.Configuration;
using OctoPoC.Core.Projects;
using OctoPoC.Messages.Commands;
using OctoPoC.Messages.RequestResponses;

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
            Console.WriteLine("5: Get all settings");
            var option = Console.ReadLine();
            int version = 1;
            
            using (var system = ActorSystem.Create("OctopusManager", config))
            {
                var stub = system.ActorOf<StubActor>("stub");
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
                        stub.Tell(new AddAppSettingCommand(Guid.Parse("30a37555-780a-4748-962c-26a4d163f56c"), key, value, "1", DateTimeOffset.Now));
                    }
                    else if (option == "4")
                    {
                        Console.WriteLine("Enter the key");
                        var key = Console.ReadLine();
                        Console.WriteLine("Enter the value");
                        var value = Console.ReadLine();
                        stub.Tell(new UpdateAppSettingCommand(Guid.Parse("30a37555-780a-4748-962c-26a4d163f56c"), key, value, "1", DateTimeOffset.Now));
                    }
                    else if (option == "5")
                    {
                        var response = stub.Ask<GetAllAppSettingsResponse>(new GetAllAppSettingsRequest(Guid.Parse("30a37555-780a-4748-962c-26a4d163f56c"))).Result;
                        Console.WriteLine("The project settins");
                        foreach (var  setting in response.ProjectAppSettings)
                        {
                            Console.WriteLine($"Key: {setting.Key} Value: {setting.Value} RecordTime: {setting.RecordTime}");
                        }
                        Console.WriteLine();
                        Console.WriteLine("The project settins audit trail");
                        foreach (var setting in response.AuditableProjectAppSettings)
                        {
                            Console.WriteLine($"Key: {setting.Key} Value: {setting.Value} RecordTime: {setting.RecordTime} Operation: {setting.Operation}");
                        }
                    }
                    option = Console.ReadLine();
                }
                
            }
        }
    }
}
