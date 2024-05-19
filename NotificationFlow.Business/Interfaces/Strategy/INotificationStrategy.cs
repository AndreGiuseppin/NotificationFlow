using NotificationFlow.Business.Command;

namespace NotificationFlow.Business.Interfaces.Strategy
{
    public interface INotificationStrategy
    {
        List<string> Types { get; }

        Task Handle(NotificationCommand eventMessage);
    }
}
