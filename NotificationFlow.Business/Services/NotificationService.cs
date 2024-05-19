using Hangfire;
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
        private readonly IBackgroundJobClient _backgroundJobClient;

        public NotificationService(IKafkaProducer producer, KafkaOptions kafkaOptions, IBackgroundJobClient backgroundJobClient)
        {
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
            _kafkaOptions = kafkaOptions ?? throw new ArgumentNullException(nameof(kafkaOptions));
            _backgroundJobClient = backgroundJobClient ?? throw new ArgumentNullException(nameof(backgroundJobClient));
        }

        public async Task Post(NotificationRequest request)
        {
            if (request.ScheduleTime is not null)
            {
                _backgroundJobClient.Schedule(() => ProduceAsync(request), request.ScheduleTime.Value);
                return;
            }

            await ProduceAsync(request);

            return;
        }

        public async Task ProduceAsync<T>(T request)
        {
            await _producer.Producer(request, _kafkaOptions.NotificationTopic);
        }
    }
}
