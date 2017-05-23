using System.Collections.Generic;

namespace OctoPoC.Messages.RequestResponses
{
    public class GetAllAppSettingsResponse
    {
        public IEnumerable<ProjectAppSettingDto> ProjectAppSettings { get; }
        public IEnumerable<ProjectAppSettingDto> AuditableProjectAppSettings { get; }
        public IEnumerable<ProjectAppSettingDiffDto> Difference { get; }

        public GetAllAppSettingsResponse(IEnumerable<ProjectAppSettingDto> projectAppSettings, IEnumerable<ProjectAppSettingDto> auditableProjectAppSettings, IEnumerable<ProjectAppSettingDiffDto> difference)
        {
            ProjectAppSettings = projectAppSettings;
            AuditableProjectAppSettings = auditableProjectAppSettings;
            Difference = difference;
        }
    }
}
