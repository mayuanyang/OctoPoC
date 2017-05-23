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
            var response = new GetAllAppSettingsResponse(normalSettings, auditableSettings);
            Sender.Tell(response);
        }
    }
}
