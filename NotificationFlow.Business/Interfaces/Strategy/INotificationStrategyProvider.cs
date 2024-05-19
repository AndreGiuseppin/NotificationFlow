namespace NotificationFlow.Business.Interfaces.Strategy
{
    public interface INotificationStrategyProvider
    {
        INotificationStrategy Get(string type);
    }
}
