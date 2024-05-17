using NotificationFlow.Business.Entity;

namespace NotificationFlow.Business.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> Post(Notification notification);
    }
}
