
namespace Api;

using Api.Data;
using Api.Errors;
using Api.Helpers;
using Api.Interfaces;
using Api.Repository;
using Microsoft.EntityFrameworkCore;

public static class DependencyInjection
{
  public static IServiceCollection AppPresentation(this IServiceCollection services, ConfigurationManager configuration)
  {
    services
      .AddProblemDetails(options => options.CustomizeProblemDetails = CustomProblemDetails.Customize)
      .AddEndpointsApiExplorer()
      .AddExceptionHandler<ExceptionsToProblemDetailsHandler>()
      .AddAutoMapper(typeof(Program))
      .AddScoped(typeof(SkillHelper))
      .AddScoped(typeof(IJobRepository), typeof(JobRepository))
      .AddScoped(typeof(ISkillRepository), typeof(SkillRepository))
      .AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(
          configuration.GetConnectionString("DefaultConnection")
        ));
    return services;
  }
}
