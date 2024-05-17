using NotificationFlow.Business.Models;

namespace NotificationFlow.Business.Interfaces.Services
{
    public interface IUserService
    {
        Task Post(UserRequest request);
        Task PatchNotificationPreferences(UserNotificationPreferencesRequest request);
    }
}
