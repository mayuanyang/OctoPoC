﻿using System;
using Akka.Actor;
using OctoPoC.Messages.Events;

namespace OctoPoC.Core.Projects
{
    public partial class ProjectActor
    {
        private void ApplyChange(AppSettingAddedEvent evt)
        {
            Console.WriteLine(
                $"New setting is added, Key: {evt.Key} Value: {evt.Value}");
            _sender.Tell(evt);
            _appSettingAuditableActor.Tell(evt);
            _appSettingNonAuditableActor.Tell(evt);
        }

        private void Apply(AppSettingAddedEvent evt)
        {
            // Nothing need to be done now
        }
    }
}
