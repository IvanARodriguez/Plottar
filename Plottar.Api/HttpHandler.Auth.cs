namespace Plottar.Api.HttpHandler;

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
    endpoints.MapPost("/auth/register", async (RegisterRequest req, ISender mediator) =>
     {
       var command = new RegisterCommand(req.FirstName, req.LastName, req.Email, req.Password);
       var authResult = await mediator.Send(command);

       return authResult.Match(
        result => Results.Ok(MapResult(result)),
        errors => ErrorHandling.Problem(errors)
       );
     });

    endpoints.MapPost("/auth/login", async (LoginRequest req, ISender mediator) =>
    {
      var query = new LoginQuery(req.Email, req.Password);
      var authResult = await mediator.Send(query);
      if (authResult.IsError)
      {

      }
      return authResult.Match(
        result => Results.Ok(MapResult(result)),
        errors => ErrorHandling.Problem(errors)
      );
    });
  }
  private static AuthenticationResponse MapResult(AuthenticationResult result)
  {
    return new AuthenticationResponse(
      result.User.Id,
      result.User.FirstName,
      result.User.LastName,
      result.User.Email,
      result.Token
    );
  }
}
