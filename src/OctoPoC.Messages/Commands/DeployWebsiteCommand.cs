using OctoPoC.Messages.MessageContracts;

namespace OctoPoC.Messages.Commands
{
    public class DeployWebsiteCommand : ICommand
    {
        public string WebsiteName { get; }
        public string ApplicationPoolName { get; }
        public string Port { get; }
        public string Version { get; }
        public byte[] Content { get; }

        public DeployWebsiteCommand(string websiteName, string applicationPoolName, string port, string version, byte[] content)
        {
            WebsiteName = websiteName;
            ApplicationPoolName = applicationPoolName;
            Port = port;
            Version = version;
            Content = content;
        }
    }
}
