using NotificationFlow.Business.Entity;

namespace NotificationFlow.Business.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task Post(User user);
        Task<User> Get(int userId);
        Task PatchNotificationPreferences(int userId, bool ReceiveGeneralNotifications, bool ReceiveSpecificNotifications);
        Task<List<User>> GetUsersWithGeneralNotificationPreferencesEnabled();
    }
}
