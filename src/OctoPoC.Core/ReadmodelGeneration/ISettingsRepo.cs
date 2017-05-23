using OctoPoC.Messages.Events;

namespace OctoPoC.Core.ReadmodelGeneration
{
    public interface ISettingsRepo
    {
        void Add(AppSettingAddedEvent evt);
        void Update(AppSettingUpdatedEvent evt);
    }
}
