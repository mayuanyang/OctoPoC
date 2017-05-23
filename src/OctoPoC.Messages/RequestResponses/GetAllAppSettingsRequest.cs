using System;

namespace OctoPoC.Messages.RequestResponses
{
    public class GetAllAppSettingsRequest
    {
        public Guid ProjectId { get; }

        public GetAllAppSettingsRequest(Guid projectId)
        {
            ProjectId = projectId;
        }
    }
}
