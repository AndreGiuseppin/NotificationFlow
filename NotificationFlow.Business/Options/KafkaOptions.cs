namespace NotificationFlow.Business.Options
{
    public class KafkaOptions
    {
        public string GroupId { get; set; }
        public string BootstrapServers { get; set; }
        public string SecurityProtocol { get; set; }
        public string NotificationTopic { get; set; }
    }
}
