using NotificationFlow.Business.Interfaces.Repositories;
using NotificationFlow.Business.Interfaces.Services;
using NotificationFlow.Business.Services;
using NotificationFlow.Business.Services.NotificationDecorator;
using NotificationFlow.Data.DbContexts.SqlServer.Repositories;

namespace NotificationFlow.Api.Extensions.ApplicationService
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<INotificationService, NotificationServiceWithUser>();
            services.Decorate<INotificationService, NotificationService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationUsersRepository, NotificationUsersRepository>();

            return services;
        }
    }
}