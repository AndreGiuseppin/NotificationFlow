using Confluent.Kafka;
using NotificationFlow.Business.Interfaces.Producer;

namespace NotificationFlow.Business.Producer
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducer(IProducer<Null, string> producer)
        {
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }

        public async Task Producer<T>(T message, string topic)
        {
            string messageSerialized = System.Text.Json.JsonSerializer.Serialize(message);
            await _producer.ProduceAsync(topic, new Message<Null, string>
            {
                Value = messageSerialized
            });

            return;
        }
    }
}