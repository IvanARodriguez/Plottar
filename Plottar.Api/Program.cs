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
  app.UseExceptionHandler("/error");


  app.UseHttpsRedirection();
  app.MapAuthRoutes();
  app.Run();
}
