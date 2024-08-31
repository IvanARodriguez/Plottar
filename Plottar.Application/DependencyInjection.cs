namespace Plottar.Application;

using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Plottar.Application.Common.Behaviors;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));

    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    services.AddScoped(
      typeof(IPipelineBehavior<,>),
      typeof(ValidationBehavior<,>));

    return services;
  }
}
