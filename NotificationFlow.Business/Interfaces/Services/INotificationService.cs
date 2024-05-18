using NotificationFlow.Business.Models;

namespace NotificationFlow.Business.Interfaces.Services
{
    public interface INotificationService
    {
        Task Post(NotificationRequest request);
    }
}
