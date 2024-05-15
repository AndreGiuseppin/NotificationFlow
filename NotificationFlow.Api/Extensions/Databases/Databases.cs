using Microsoft.EntityFrameworkCore;
using NotificationFlow.Data.Database.SqlServer;

namespace NotificationFlow.Api.Extensions.Databases
{
    public static class Databases
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration["Databases:ConnectionString"].ToString()));

            return services;
        }
    }
}
