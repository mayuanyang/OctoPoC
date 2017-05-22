using System;
using Akka.Actor;
using OctoPoC.Messages.Commands;
using OctoPoC.Messages.Events;

namespace OctoPoC.Core.PowerShell
{
    public class PowerShellActor : TypedActor, IHandle<ExecutePowerShellCommand>, IHandle<PowerShellExecutedEvent>
    {
        public void Handle(ExecutePowerShellCommand message)
        {
            Console.WriteLine($"Powershell script is about to execute for Id {message.Id}");
            using (var powerShell = System.Management.Automation.PowerShell.Create())
            {
                
                powerShell.AddScript(message.Script);
                if (message.Parameters != null)
                {
                    foreach (var param in message.Parameters)
                    {
                        powerShell.AddParameter(param.Key, param.Value);
                    }
                }
                
                powerShell.Invoke();
            }

            Sender.Tell(new PowerShellExecutedEvent(message.Id));
        }

        public void Handle(PowerShellExecutedEvent message)
        {
            Console.WriteLine($"Powershell script is executed for Id {message.Id}");
        }
    }
}
