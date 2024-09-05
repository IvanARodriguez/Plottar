using Api.Data;
using Api.Dtos;
using Api.HttpHandlers;
using Api.Repository.Common;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped(typeof(IRepository<JobDto>), typeof(Api.Repository.JobRepository));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")
  )
);

var app = builder.Build();
app.MapGet("/test", () => "Test working");
app.MapJobEndpoints();
app.UseHttpsRedirection();
app.Run();
