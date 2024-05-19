using Microsoft.Extensions.Logging;
using NotificationFlow.Business.Command;
using NotificationFlow.Business.Interfaces.Repositories;
using NotificationFlow.Business.Interfaces.Services;
using NotificationFlow.Business.Interfaces.Strategy;
using NotificationFlow.Business.Mappers;

namespace NotificationFlow.Business.Services.NotificationDecorator
{
    public class NotificationServiceWithUser : INotificationServiceCommand
    {
        private readonly IUserRepository _userRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationUsersRepository _notificationUsersRepository;
        private readonly ILogger _logger;
        private readonly INotificationStrategyProvider _strategyProvider;
        public NotificationServiceWithUser(IUserRepository userRepository, INotificationRepository notificationRepository,
            INotificationUsersRepository notificationUsersRepository, ILogger logger, INotificationStrategyProvider strategyProvider)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
            _notificationUsersRepository = notificationUsersRepository ?? throw new ArgumentNullException(nameof(notificationUsersRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _strategyProvider = strategyProvider ?? throw new ArgumentNullException(nameof(strategyProvider));
        }

        public async Task ProcessAsync(NotificationCommand command)
        {
            try
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

                command.GetNotificationTypes().ForEach(type =>
                {
                    _strategyProvider.Get(type)
                    .Handle(command);
                });

                return;
            }
            catch (Exception ex)
            {
                _logger.LogError("{serviceName} - An error occur. Error: {error}. " +
                    "NotificationId: {notificationId}. Title: {title}. UserId: {userId}, ScheduleTime: {schedule}"
                    , "NotificationServiceWithUser", ex.Message, command.NotificationId, command.Title, command.UserId, command.ScheduleTime);
                return;
            }
        }
    }
}
