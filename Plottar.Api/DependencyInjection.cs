namespace Plottar.Api;

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Plottar.Api.Errors;
using Plottar.Api.Common.Mapping;

public static class DependencyInjection
{
  public static IServiceCollection AddPresentation(this IServiceCollection services)
  {
    services.AddMappings();
    services.AddSingleton<ProblemDetailsFactory, PlottarProblemDetailsFactory>();

    return services;
  }
}
