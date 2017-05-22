using System;
using System.Collections.Generic;
using OctoPoC.Messages.MessageContracts;

namespace OctoPoC.Messages.Commands
{
    public class ExecutePowerShellCommand : ICommand
    {
        public Guid Id { get; }
        public string Script { get; }
        public Dictionary<string, string> Parameters { get; }

        public ExecutePowerShellCommand(Guid id, string script, Dictionary<string, string> parameters)
        {
            Id = id;
            Script = script;
            Parameters = parameters;
        }
    }
}
