using Hangfire;
using Hangfire.SqlServer;

namespace NotificationFlow.Api.Extensions.HangFire
{
    public static class HangFire
    {
        public static IServiceCollection AddHangFire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(conf => conf
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseFilter(new AutomaticRetryAttribute
            {
                Attempts = 3,
                DelaysInSeconds = new int[] { 5 }
            })
            .UseSqlServerStorage(configuration["Databases:HangFireConnection"].ToString(), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

            services.AddHangfireServer();

            return services;
        }
    }
}
