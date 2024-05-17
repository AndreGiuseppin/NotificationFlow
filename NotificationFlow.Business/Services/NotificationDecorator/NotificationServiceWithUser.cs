using NotificationFlow.Business.Command;
using NotificationFlow.Business.Interfaces.Repositories;
using NotificationFlow.Business.Interfaces.Services;
using NotificationFlow.Business.Mappers;

namespace NotificationFlow.Business.Services.NotificationDecorator
{
    public class NotificationServiceWithUser : INotificationService
    {
        private readonly IUserRepository _userRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationUsersRepository _notificationUsersRepository;

        public NotificationServiceWithUser(IUserRepository userRepository, INotificationRepository notificationRepository,
            INotificationUsersRepository notificationUsersRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
            _notificationUsersRepository = notificationUsersRepository ?? throw new ArgumentNullException(nameof(notificationUsersRepository));
        }

        public async Task Post(NotificationCommand command)
        {
            var user = await _userRepository.Get(command.UserId);

            if (command.IsGeneral is false)
            {
                if (user.NotificationPreference.ReceiveSpecificNotifications)
                {
                    var notification = await _notificationRepository.Post(NotificationMapper.NotificationCommandToNotificationGeneral(command));
                    await _notificationUsersRepository.Post(NotificationUserMapper.ToNotificationUser(notification.Id, command.UserId));

                    return;
                }
            }

            var users = await _userRepository.GetUsersWithGeneralNotificationPreferencesEnabled();
            if (users.Count == 0) return;

            await _notificationUsersRepository.BulkPost(NotificationUserMapper.ToNotificationUsers(command.NotificationId, users.Select(x => x.Id).ToList()));

            return;
        }
    }
}
