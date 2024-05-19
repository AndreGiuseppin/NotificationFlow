using NotificationFlow.Business.Command;
using NotificationFlow.Business.Commons;
using NotificationFlow.Business.Interfaces.Strategy;

namespace NotificationFlow.Business.Services.Strategy
{
    public class NotificationWhenHaveSms : INotificationStrategy
    {
        public List<string> Types => new List<string> { NotificationTypes.SmsNotification };

        public async Task Handle(NotificationCommand eventMessage)
        {
            // Sms rules
            return;
        }
    }
}
