using Plottar.Api.HttpHandler;
using Plottar.Application;
using Plottar.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
  builder.Configuration.AddUserSecrets<Program>();
  builder.Services
    .AddApplication()
    .AddEndpointsApiExplorer()
    .AddInfrastructure(builder.Configuration);
}


var app = builder.Build();
{
  app.UseHttpsRedirection();
  app.MapAuthRoutes();
  app.Run();
}

