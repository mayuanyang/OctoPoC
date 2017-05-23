using System.Collections.Generic;

namespace OctoPoC.Messages.RequestResponses
{
    public class GetAllAppSettingsResponse
    {
        public IEnumerable<ProjectAppSettingDto> ProjectAppSettings { get; }
        public IEnumerable<ProjectAppSettingDto> AuditableProjectAppSettings { get; }

        public GetAllAppSettingsResponse(IEnumerable<ProjectAppSettingDto> projectAppSettings, IEnumerable<ProjectAppSettingDto> auditableProjectAppSettings)
        {
            ProjectAppSettings = projectAppSettings;
            AuditableProjectAppSettings = auditableProjectAppSettings;
        }
    }
}
