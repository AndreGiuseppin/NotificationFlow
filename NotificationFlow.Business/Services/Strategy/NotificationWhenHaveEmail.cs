using NotificationFlow.Business.Command;
using NotificationFlow.Business.Commons;
using NotificationFlow.Business.Interfaces.Strategy;

namespace NotificationFlow.Business.Services.Strategy
{
    public class NotificationWhenHaveEmail : INotificationStrategy
    {
        public List<string> Types => new List<string> { NotificationTypes.EmailNotification };

        public async Task Handle(NotificationCommand eventMessage)
        {
            // Email rules
            return;
        }
    }
}
