using NotificationFlow.Api.Consumer;
using NotificationFlow.Business.Interfaces.Producer;
using NotificationFlow.Business.Interfaces.Repositories;
using NotificationFlow.Business.Interfaces.Services;
using NotificationFlow.Business.Producer;
using NotificationFlow.Business.Services;
using NotificationFlow.Business.Services.NotificationDecorator;
using NotificationFlow.Data.DbContexts.SqlServer.Repositories;

namespace NotificationFlow.Api.Extensions.ApplicationService
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHostedService<NotificationConsumer>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IKafkaProducer, KafkaProducer>();

            services.AddScoped<INotificationServiceCommand, NotificationServiceWithUser>();
            services.Decorate<INotificationServiceCommand, NotificationServiceWithNotification>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationUsersRepository, NotificationUsersRepository>();

            return services;
        }

        public static IServiceCollection AddILogger(this IServiceCollection services)
        {
            services.AddSingleton<ILogger>(provider => provider.GetService<ILogger<Program>>());

            return services;
        }
    }
}