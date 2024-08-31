namespace Plottar.Api;

using Plottar.Api.Common.Mapping;
using Plottar.Api.Errors;

public static class DependencyInjection
{
  public static IServiceCollection AddPresentation(this IServiceCollection services)
  {
    services.AddMappings();
    services.AddProblemDetails(options => options.CustomizeProblemDetails = CustomProblemDetails.Customize);
    return services;
  }
}
