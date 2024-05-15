using NotificationFlow.Api.Extensions.ApplicationService;
using NotificationFlow.Api.Extensions.Databases;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddServices()
    .AddRepositories()
    .AddSqlServer()
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