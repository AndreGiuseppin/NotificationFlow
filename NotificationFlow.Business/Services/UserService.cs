using NotificationFlow.Business.Command;
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

        public async Task PatchNotificationPreferences(UserNotificationPreferencesRequest request) =>
            await _userRepository.PatchNotificationPreferences(request.UserId, request.ReceiveGeneralNotifications, request.ReceiveSpecificNotifications);

        public async Task<GetUserNotificationsResponse> GetUserNotifications(GetUserNotificationsCommand request)
        {
            var user = await _userRepository.Get(request.UserId);

            if (user is null)
                return new GetUserNotificationsResponse();

            return UserMapper.UserToUserNotification(user);
        }
    }
}
