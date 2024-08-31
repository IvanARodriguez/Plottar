using Plottar.Api;
using Plottar.Api.HttpHandler;
using Plottar.Application;
using Plottar.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
  builder.Configuration.AddUserSecrets<Program>();

  builder.Services
    .AddPresentation()
    .AddApplication()
    .AddEndpointsApiExplorer()
    .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
  // Exception Handling
  app.UseExceptionHandler("/error");
  app.UsePathBase("/api");
  app.MapAuthRoutes();
  app.MapJobsRoutes();
  app.Run();
}
