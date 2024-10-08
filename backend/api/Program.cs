using Api.HttpHandlers;

using Api;

var builder = WebApplication.CreateBuilder(args);
{
  builder.Services
  .AppPresentation(builder.Configuration);
}

var app = builder.Build();
{
  app.UseExceptionHandler();
  app.MapApiRoutes();
  app.Run();
}
