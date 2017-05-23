using System;
using System.Collections.Generic;
using System.Linq;
using OctoPoC.Messages.Events;

namespace OctoPoC.Core.ReadmodelGeneration
{
    class AuditableSettingsRepo : IAuditableSettingsRepo
    {
        private List<ProjectAppSetting> _settings;
        public AuditableSettingsRepo()
        {
            _settings = new List<ProjectAppSetting>();
        }
        public void Add(AppSettingAddedEvent evt)
        {
            _settings.Add(new ProjectAppSetting(evt.ProjectId, evt.Key, evt.Value, evt.Version, evt.RecordTime, "INSERT"));
        }

        public void Update(AppSettingUpdatedEvent evt)
        {
            _settings.Add(new ProjectAppSetting(evt.ProjectId, evt.Key, evt.Value, evt.Version, evt.RecordTime, "UPDATE"));
        }

        public IEnumerable<ProjectAppSetting> GetAllAppSettings(Guid projectId)
        {
            return _settings.Where(x => x.ProjectId == projectId).OrderBy(y => y.RecordTime);
        }
    }
}