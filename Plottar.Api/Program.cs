using Plottar.Api.HttpHandler;
using Plottar.Application.Services;

var builder = WebApplication.CreateBuilder(args);
{
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
  builder.Services.AddHttpLogging(logger =>
  {
    logger.MediaTypeOptions.AddText("application/javascript");
    logger.CombineLogs = true;
  });
}


var app = builder.Build();
{
  app.UseHttpsRedirection();
  app.MapAuthRoutes();
  app.Run();
}

