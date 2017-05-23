using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using OctoPoC.Core.ReadmodelGeneration;
using OctoPoC.Messages.RequestResponses;

namespace OctoPoC.Core.ReadLayer
{
    public class AppSettingsReadActor : TypedActor, IHandle<GetAllAppSettingsRequest>
    {
        private readonly IAuditableSettingsRepo _auditableSettingsRepo;
        private readonly INonAuditableSettingsRepo _nonAuditableSettingsRepo;

        public AppSettingsReadActor(IAuditableSettingsRepo auditableSettingsRepo, INonAuditableSettingsRepo nonAuditableSettingsRepo)
        {
            _auditableSettingsRepo = auditableSettingsRepo;
            _nonAuditableSettingsRepo = nonAuditableSettingsRepo;
        }
        public void Handle(GetAllAppSettingsRequest message)
        {
            var normalSettings = _nonAuditableSettingsRepo.GetAllAppSettings(message.ProjectId)
                .Select(
                    x => new ProjectAppSettingDto(x.ProjectId, x.Key, x.Value, x.Version, x.RecordTime, x.Operation)).OrderBy(x => x.Key).ThenBy(x => x.RecordTime).ToList();

            var auditableSettings = _auditableSettingsRepo.GetAllAppSettings(message.ProjectId)
                .Select(
                    x => new ProjectAppSettingDto(x.ProjectId, x.Key, x.Value, x.Version, x.RecordTime, x.Operation)).OrderBy(x => x.Key).ThenBy(x => x.RecordTime).ToList();

            IList<ProjectAppSettingDiffDto> difference = null;
            if (auditableSettings.Count == normalSettings.Count)
            {
                difference = normalSettings
                    .Select(x => new ProjectAppSettingDiffDto(x.ProjectId, x.Key, x.Value, x.Value, x.Version,
                        x.RecordTime, x.Operation))
                    .OrderBy(x => x.Key)
                    .ThenBy(x => x.RecordTime)
                    .ToList();
            }
            else
            {
                difference = new List<ProjectAppSettingDiffDto>();
                foreach (var setting in normalSettings)
                {
                    if (auditableSettings.Count(x => x.Key == setting.Key) > 1)
                    {
                        var original = auditableSettings.Single(x => x.Key == setting.Key && x.Operation == "INSERT");
                        var updated = auditableSettings.Where(x => x.Key == setting.Key)
                            .OrderByDescending(x => x.RecordTime)
                            .Take(1)
                            .Select(x => new ProjectAppSettingDiffDto(x.ProjectId, x.Key, original.Value, x.Value,
                                x.Version, x.RecordTime, x.Operation))
                            .Single();
                        difference.Add(updated);
                    }
                    else
                    {
                        var updated = auditableSettings.Where(x => x.Key == setting.Key)
                            .OrderByDescending(x => x.RecordTime)
                            .Take(1)
                            .Select(x => new ProjectAppSettingDiffDto(x.ProjectId, x.Key, x.Value, x.Value,
                                x.Version, x.RecordTime, x.Operation))
                            .Single();
                        difference.Add(updated);
                    }
                }
            }
            var response = new GetAllAppSettingsResponse(normalSettings, auditableSettings, difference);
            Sender.Tell(response);
        }
    }
}
