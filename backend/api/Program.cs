using Api.Data;
using Api.HttpHandlers;
using Microsoft.EntityFrameworkCore;

using Api;

var builder = WebApplication.CreateBuilder(args);
{
  builder.Services
  .AppPresentation()
  .AddEndpointsApiExplorer()
  .AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
      builder.Configuration.GetConnectionString("DefaultConnection")
  )
);
}

var app = builder.Build();
{
  app.UseExceptionHandler("/api/error");
  app.MapApiRoutes();
  app.Run();
}
