using Hangfire;
using Microsoft.EntityFrameworkCore;
using NotificationFlow.Api.Extensions.ApplicationService;
using NotificationFlow.Api.Extensions.Databases;
using NotificationFlow.Api.Extensions.HangFire;
using NotificationFlow.Api.Extensions.MessageBroker;
using NotificationFlow.Data.Database.SqlServer;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services
    .AddKafka(configuration)
    .AddServices()
    .AddRepositories()
    .AddILogger()
    .AddSqlServer(configuration)
    .AddHangFire(configuration)
    .AddSwaggerGen()
    .AddEndpointsApiExplorer()
    .AddControllers();

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI()
    .UseHttpsRedirection()
    .UseRouting()
    .UseAuthorization()
    .UseHangfireDashboard()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapHangfireDashboard();
    });

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
};

app.MapControllers();
app.Run();