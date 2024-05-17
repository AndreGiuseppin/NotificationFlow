using NotificationFlow.Business.Command;
using NotificationFlow.Business.Interfaces.Repositories;
using NotificationFlow.Business.Interfaces.Services;
using NotificationFlow.Business.Mappers;

namespace NotificationFlow.Business.Services.NotificationDecorator
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationService _next;

        public NotificationService(INotificationRepository notificationRepository, INotificationService next)
        {
            _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Post(NotificationCommand command)
        {
            if (command.IsGeneral is false)
            {
                await _next.Post(command);
                return;
            }

            await _notificationRepository.Post(NotificationMapper.NotificationCommandToNotificationGeneral(command));

            return;
        }
    }
}
