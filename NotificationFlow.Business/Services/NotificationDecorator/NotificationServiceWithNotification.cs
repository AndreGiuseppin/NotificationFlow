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

        public NotificationServiceWithNotification(INotificationRepository notificationRepository, INotificationServiceCommand next)
        {
            _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task ProcessAsync(NotificationCommand command)
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
    }
}
