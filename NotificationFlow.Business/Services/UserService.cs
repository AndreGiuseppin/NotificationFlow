using NotificationFlow.Business.Interfaces.Repositories;
using NotificationFlow.Business.Interfaces.Services;
using NotificationFlow.Business.Mappers;
using NotificationFlow.Business.Models;

namespace NotificationFlow.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public async Task Post(UserRequest request)
        {
            await _userRepository.Post(UserMapper.UserRequestToUser(request));
        }

        public async Task PatchNotificationPreferences(UserNotificationPreferencesRequest request)
        {
            await _userRepository.PatchNotificationPreferences(request.UserId, request.ReceiveGeneralNotifications, request.ReceiveSpecificNotifications);
        }
    }
}
