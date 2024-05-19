using Hangfire;
using NotificationFlow.Api.Extensions.ApplicationService;
using NotificationFlow.Api.Extensions.Databases;
using NotificationFlow.Api.Extensions.HangFire;
using NotificationFlow.Api.Extensions.MessageBroker;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services
    .AddKafka(configuration)
    .AddServices()
    .AddRepositories()
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

app.MapControllers();
app.Run();