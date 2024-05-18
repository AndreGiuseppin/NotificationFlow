namespace NotificationFlow.Business.Interfaces.Producer
{
    public interface IKafkaProducer
    {
        Task Producer<T>(T message, string topic);
    }
}
