using Application;
using Infrastructure;
using Presentation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
  .AddApplication()
  .AddInfrastructure()
  .AddPresentation();

builder.Host.UseSerilog((ctx, config) =>
  config.ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.Run();

