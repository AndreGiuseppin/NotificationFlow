using NotificationFlow.Business.Interfaces.Producer;
using NotificationFlow.Business.Interfaces.Services;
using NotificationFlow.Business.Models;
using NotificationFlow.Business.Options;

namespace NotificationFlow.Business.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IKafkaProducer _producer;
        private readonly KafkaOptions _kafkaOptions;

        public NotificationService(IKafkaProducer producer, KafkaOptions kafkaOptions)
        {
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
            _kafkaOptions = kafkaOptions ?? throw new ArgumentNullException(nameof(kafkaOptions));
        }

        public async Task Post(NotificationRequest request)
        {
            await _producer.Producer(request, _kafkaOptions.NotificationTopic);
            return;
        }
    }
}
