using NotificationFlow.Api.Extensions.ApplicationService;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddServices()
    .AddRepositories()
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