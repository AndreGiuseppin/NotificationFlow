using NotificationFlow.Business.Command;
using NotificationFlow.Business.Commons;
using NotificationFlow.Business.Interfaces.Strategy;

namespace NotificationFlow.Business.Services.Strategy
{
    public class NotificationWhenHavePush : INotificationStrategy
    {
        public List<string> Types => new List<string> { NotificationTypes.PushNotification };

        public async Task Handle(NotificationCommand eventMessage)
        {
            // Push rules
            return;
        }
    }
}
