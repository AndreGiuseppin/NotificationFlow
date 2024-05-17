using NotificationFlow.Business.Command;

namespace NotificationFlow.Business.Interfaces.Services
{
    public interface INotificationService
    {
        Task Post(NotificationCommand command);
    }
}
