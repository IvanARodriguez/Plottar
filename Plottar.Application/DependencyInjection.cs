namespace Plottar.Application;

using Microsoft.Extensions.DependencyInjection;
using Plottar.Application.Services;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddScoped<IAuthenticationService, AuthenticationService>();

    return services;
  }
}
