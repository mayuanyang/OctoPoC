using OctoPoC.Messages.MessageContracts;

namespace OctoPoC.Messages.Events
{
    public class WebsiteDeployedEvent : IEvent
    {
        public string WebsiteName { get; }

        public WebsiteDeployedEvent(string websiteName)
        {
            WebsiteName = websiteName;
        }
    }
}
