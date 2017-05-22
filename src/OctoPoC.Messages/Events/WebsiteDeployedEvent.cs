using OctoPoC.Messages.MessageContracts;

namespace OctoPoC.Messages.Events
{
    public class WebsiteDeployedEvent : IEvent
    {
        public string WebsiteName { get; }
        public string Version { get; }

        public WebsiteDeployedEvent(string websiteName, string version)
        {
            WebsiteName = websiteName;
            Version = version;
        }
    }
}
