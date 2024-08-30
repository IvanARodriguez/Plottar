namespace Plottar.Api.HttpHandler;

using ErrorOr;
using MapsterMapper;
using MediatR;
using Plottar.Api.Errors;
using Plottar.Application.Commands.Register;
using Plottar.Application.Common;
using Plottar.Application.Queries.Login;
using Plottar.Contracts.Authentication;

public static class AuthenticationHandler
{
  public static void MapAuthRoutes(this IEndpointRouteBuilder endpoints)
  {
    endpoints.MapPost("/auth/register", async (RegisterRequest req, ISender mediator, IMapper mapper) =>
     {
       var command = mapper.Map<RegisterCommand>(req);
       var authResult = await mediator.Send(command);

       return authResult.Match(
        result => Results.Ok(mapper.Map<AuthenticationResponse>(result)),
        errors => ErrorHandling.Problem(errors)
       );
     });

    endpoints.MapPost("/auth/login", async (LoginRequest req, ISender mediator, IMapper mapper) =>
    {
      var query = mapper.Map<LoginQuery>(req);
      var authResult = await mediator.Send(query);
      if (authResult.IsError)
      {

      }
      return authResult.Match(
        result => Results.Ok(mapper.Map<AuthenticationResponse>(result)),
        errors => ErrorHandling.Problem(errors)
      );
    });
  }

}
