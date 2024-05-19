using NotificationFlow.Business.Interfaces.Repositories;
using NotificationFlow.Business.Interfaces.Services;
using NotificationFlow.Business.Mappers;
using NotificationFlow.Business.Models;

namespace NotificationFlow.Business.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        public async Task Post(UserRequest request) =>
            await _userRepository.Post(UserMapper.UserRequestToUser(request));

        public async Task PatchNotificationPreferences(int userId, UserNotificationPreferencesRequest request) =>
            await _userRepository.PatchNotificationPreferences(userId, request.ReceiveGeneralNotifications, request.ReceiveSpecificNotifications);

        public async Task<GetUserNotificationsResponse> GetUserNotifications(int userId)
        {
            var user = await _userRepository.Get(userId);

            if (user is null)
                return new GetUserNotificationsResponse();

            return UserMapper.UserToUserNotification(user);
        }
    }
}
