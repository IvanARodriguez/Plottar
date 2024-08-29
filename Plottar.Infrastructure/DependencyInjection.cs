namespace Plottar.Infrastructure;

using Plottar.Application.Common.Interfaces.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Plottar.Infrastructure.Authentication;
using Plottar.Application.Common.Interfaces.Services;
using Plottar.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Plottar.Application.Common.Interfaces.Persistence;
using Plottar.Infrastructure.Persistence;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
  {
    services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
    services.AddSingleton<IJwtGenerator, JwtGenerator>();
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    services.AddScoped<IUserRepository, UserRepository>();
    return services;
  }
}
