using Microsoft.Extensions.Logging;
using NotificationFlow.Business.Command;
using NotificationFlow.Business.Interfaces.Repositories;
using NotificationFlow.Business.Interfaces.Services;
using NotificationFlow.Business.Mappers;

namespace NotificationFlow.Business.Services.NotificationDecorator
{
    public class NotificationServiceWithNotification : INotificationServiceCommand
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationServiceCommand _next;
        private readonly ILogger _logger;

        public NotificationServiceWithNotification(INotificationRepository notificationRepository,
            INotificationServiceCommand next, ILogger logger)
        {
            _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task ProcessAsync(NotificationCommand command)
        {
            try
            {
                if (command.IsGeneral is false)
                {
                    await _next.ProcessAsync(command);
                    return;
                }

                var notification = await _notificationRepository.Post(NotificationMapper.NotificationCommandToNotificationGeneral(command));
                command.WithNotificationId(notification.Id);

                await _next.ProcessAsync(command);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError("{serviceName} - An error occur. Error: {error}. " +
                    "NotificationId: {notificationId}. Title: {title}. UserId: {userId}, ScheduleTime: {schedule}"
                    , "NotificationServiceWithNotification", ex.Message, command.NotificationId, command.Title, command.UserId, command.ScheduleTime);
                return;
            }
        }
    }
}
