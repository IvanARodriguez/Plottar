using Microsoft.AspNetCore.Mvc.Infrastructure;
using Plottar.Api.Errors;
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

  builder.Services.AddSingleton<ProblemDetailsFactory, PlottarProblemDetailsFactory>();
}

var app = builder.Build();
{
  app.UseExceptionHandler("/error");


  app.UseHttpsRedirection();
  app.MapAuthRoutes();
  app.Run();
}
