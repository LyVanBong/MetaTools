using MetaTools.Services.Interfaces;

namespace MetaTools.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
