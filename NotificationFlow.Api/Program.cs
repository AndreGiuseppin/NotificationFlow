using NotificationFlow.Api.Extensions.ApplicationService;
using NotificationFlow.Api.Extensions.Databases;
using NotificationFlow.Api.Extensions.MessageBroker;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services
    .AddKafka(configuration)
    .AddServices()
    .AddRepositories()
    .AddSqlServer(configuration)
    .AddSwaggerGen()
    .AddEndpointsApiExplorer()
    .AddControllers();

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI()
    .UseHttpsRedirection()
    .UseAuthorization();

app.MapControllers();
app.Run();