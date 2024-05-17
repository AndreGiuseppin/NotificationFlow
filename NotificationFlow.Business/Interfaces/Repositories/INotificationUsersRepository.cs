using NotificationFlow.Business.Entity;

namespace NotificationFlow.Business.Interfaces.Repositories
{
    public interface INotificationUsersRepository
    {
        Task Post(NotificationUser notificationUser);
        Task BulkPost(List<NotificationUser> notificationUsers);
    }
}
