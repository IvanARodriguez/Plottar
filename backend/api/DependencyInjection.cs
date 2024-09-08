
namespace Api;

using Api.Errors;
using Api.Helpers;
using Api.Interfaces;
using Api.Repository;

public static class DependencyInjection
{
  public static IServiceCollection AppPresentation(this IServiceCollection services)
  {
    services
      .AddProblemDetails(options => options.CustomizeProblemDetails = CustomProblemDetails.Customize)
      .AddAutoMapper(typeof(Program))
      .AddScoped(typeof(SkillHelper))
      .AddScoped(typeof(IJobRepository), typeof(JobRepository))
      .AddScoped(typeof(ISkillRepository), typeof(SkillRepository));
    return services;
  }
}
