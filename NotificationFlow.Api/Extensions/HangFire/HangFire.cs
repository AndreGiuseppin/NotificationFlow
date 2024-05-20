using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Data.SqlClient;

namespace NotificationFlow.Api.Extensions.HangFire
{
    public static class HangFire
    {
        public static IServiceCollection AddHangFire(this IServiceCollection services, IConfiguration configuration)
        {
            CreateHangfireDatabase(configuration);

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

        private static void CreateHangfireDatabase(IConfiguration configuration)
        {
            string dbName = "Hangfire";
            string connectionStringFormat = configuration["Databases:SqlServerConnection"].ToString();

            using (var connection = new SqlConnection(string.Format(connectionStringFormat, "master")))
            {
                connection.Open();

                using (var command = new SqlCommand(string.Format(
                    @"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{0}') 
                                    create database [{0}];
                      ", dbName), connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
