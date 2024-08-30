namespace Plottar.Application;

using ErrorOr;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Plottar.Application.Commands.Register;
using Plottar.Application.Common;
using Plottar.Application.Common.Behaviors;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));
    services.AddScoped<
      IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>,
      ValidateRegisterCommandBehavior>();
    return services;
  }
}
