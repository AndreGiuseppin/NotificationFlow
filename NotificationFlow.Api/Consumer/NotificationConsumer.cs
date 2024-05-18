
using Confluent.Kafka;
using Newtonsoft.Json;
using NotificationFlow.Business.Command;
using NotificationFlow.Business.Interfaces.Services;
using NotificationFlow.Business.Options;

namespace NotificationFlow.Api.Consumer
{
    public class NotificationConsumer : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly KafkaOptions _options;
        private readonly IServiceScope _scope;

        public NotificationConsumer(IConsumer<Ignore, string> consumer, KafkaOptions options, IServiceProvider serviceScopeFactory)
        {
            _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _scope = serviceScopeFactory.CreateScope();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            try
            {
                var context = _scope.ServiceProvider.GetRequiredService<INotificationServiceCommand>();

                _consumer.Subscribe(_options.NotificationTopic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = _consumer.Consume(stoppingToken);

                    if (result is null || result.IsPartitionEOF || stoppingToken.IsCancellationRequested)
                        continue;

                    var message = result.Message.Value;

                    var response = JsonConvert.DeserializeObject<NotificationCommand>(message);

                    if (response is not null) await context.ProcessAsync(response);

                    _consumer.Commit(result);
                    _consumer.StoreOffset(result);
                }
            }
            catch (Exception)
            {
                _consumer.Close();
                _consumer.Dispose();
            }
        }
    }
}
