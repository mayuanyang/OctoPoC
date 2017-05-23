using System;
using System.Collections.Generic;
using System.Linq;
using OctoPoC.Messages.Events;

namespace OctoPoC.Core.ReadmodelGeneration
{
    class NonAuditableSettingsRepo : INonAuditableSettingsRepo
    {
        private List<ProjectAppSetting> _settings;
        public NonAuditableSettingsRepo()
        {
            _settings = new List<ProjectAppSetting>();
        }
        public void Add(AppSettingAddedEvent evt)
        {
            _settings.Add(new ProjectAppSetting(evt.ProjectId, evt.Key, evt.Value, evt.Version, evt.RecordTime, ""));
        }

        public void Update(AppSettingUpdatedEvent evt)
        {

            var setting = _settings.Single(x => x.Key == evt.Key);
            setting.Value = evt.Value;
            setting.RecordTime = evt.RecordTime;
            setting.Version = evt.Version;
        }

        public IEnumerable<ProjectAppSetting> GetAllAppSettings(Guid projectId)
        {
            return _settings.Where(x => x.ProjectId == projectId).OrderBy(y => y.RecordTime);
        }

    }
}