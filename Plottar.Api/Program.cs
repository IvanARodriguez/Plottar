using Plottar.Api.HttpHandler;
using Plottar.Application;
using Plottar.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
  builder.Services
    .AddApplication()
    .AddEndpointsApiExplorer()
    .AddInfrastructure();
}


var app = builder.Build();
{
  app.UseHttpsRedirection();
  app.MapAuthRoutes();
  app.Run();
}

