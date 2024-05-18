using Confluent.Kafka;
using NotificationFlow.Business.Options;
using System.Net;
using Partitioner = Confluent.Kafka.Partitioner;

namespace NotificationFlow.Api.Extensions.MessageBroker
{
    public static class MessageBroker
    {
        public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
        {
            var kafka = new KafkaOptions();
            configuration.GetSection("Kafka").Bind(kafka);
            services.AddSingleton(x => kafka);

            var configConsumer = new ConsumerConfig
            {
                GroupId = kafka.GroupId,
                BootstrapServers = kafka.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                PartitionAssignmentStrategy = PartitionAssignmentStrategy.RoundRobin,
                EnableAutoCommit = false,
                EnableAutoOffsetStore = false,
                MaxPollIntervalMs = 600000,
                SecurityProtocol = (SecurityProtocol)Enum.Parse(typeof(SecurityProtocol), kafka.SecurityProtocol)
            };

            var configProducer = new ProducerConfig
            {
                BootstrapServers = kafka.BootstrapServers,
                ClientId = Dns.GetHostName(),
                SecurityProtocol = (SecurityProtocol)Enum.Parse(typeof(SecurityProtocol), kafka.SecurityProtocol),
                Partitioner = Partitioner.ConsistentRandom
            };

            services.AddTransient(_ => new ConsumerBuilder<Ignore, string>(configConsumer).Build());
            services.AddTransient(_ => new ProducerBuilder<Null, string>(configProducer).Build());

            return services;
        }
    }
}
