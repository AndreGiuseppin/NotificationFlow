using NotificationFlow.Business.Command;

namespace NotificationFlow.Business.Interfaces.Services
{
    public interface INotificationServiceCommand
    {
        Task ProcessAsync(NotificationCommand command);
    }
}
