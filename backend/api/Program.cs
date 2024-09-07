using Api.Data;
using Api.HttpHandlers;
using Microsoft.EntityFrameworkCore;
using Api.Repository;
using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCustomProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped(typeof(IJobRepository), typeof(JobRepository));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")
  )
);

var app = builder.Build();

app.MapGet("/test", () => "Test working");

app.UseExceptionHandler("/api/error");
app.MapApiRoutes();

app.Run();
