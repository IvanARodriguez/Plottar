namespace Plottar.Infrastructure;

using Plottar.Application.Common.Interfaces.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Plottar.Infrastructure.Authentication;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services)
  {
    services.AddSingleton<IJwtGenerator, JwtGenerator>();
    return services;
  }
}
