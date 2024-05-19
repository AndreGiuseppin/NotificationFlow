using NotificationFlow.Business.Interfaces.Strategy;

namespace NotificationFlow.Business.Services.Strategy
{
    public class NotificationStrategyProvider : INotificationStrategyProvider
    {
        private readonly IEnumerable<INotificationStrategy> _handlers;

        public NotificationStrategyProvider(IEnumerable<INotificationStrategy> handlers)
           => _handlers = handlers ?? throw new ArgumentNullException(nameof(handlers));

        public INotificationStrategy Get(string type)
            => _handlers.First(x => x.Types.Contains(type));
    }
}