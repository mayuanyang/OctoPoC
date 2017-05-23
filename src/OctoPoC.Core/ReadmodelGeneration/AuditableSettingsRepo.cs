using System.Collections.Generic;
using System.Linq;
using OctoPoC.Messages.Events;

namespace OctoPoC.Core.ReadmodelGeneration
{
    class AuditableSettingsRepo : IAuditableSettingsRepo
    {
        private List<SettingDto> _settings;
        public AuditableSettingsRepo()
        {
            _settings = new List<SettingDto>();
        }
        public void Add(AppSettingAddedEvent evt)
        {
            _settings.Add(new SettingDto(evt.ProjectId, evt.Key, evt.Value, evt.Version, evt.RecordTime, "INSERT"));
        }

        public void Update(AppSettingUpdatedEvent evt)
        {
            _settings.Add(new SettingDto(evt.ProjectId, evt.Key, evt.Value, evt.Version, evt.RecordTime, "UPDATE"));
        }
    }
}