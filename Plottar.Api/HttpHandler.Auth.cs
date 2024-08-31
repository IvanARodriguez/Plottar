namespace Plottar.Api.HttpHandler;

using MapsterMapper;
using MediatR;
using Plottar.Api.Errors;
using Plottar.Application.Commands.Register;
using Plottar.Application.Queries.Login;
using Plottar.Contracts.Authentication;

public static class AuthenticationHandler
{
  public static void MapAuthRoutes(this IEndpointRouteBuilder endpoints)
  {
    var authRoutesGroup = endpoints.MapGroup("/auth");

    authRoutesGroup.MapPost("/register", async (
      HttpContext ctx,
      RegisterRequest req,
      ISender mediator,
      IMapper mapper) =>
     {
       var command = mapper.Map<RegisterCommand>(req);
       var authResult = await mediator.Send(command);

       return authResult.Match(
        result => Results.Ok(mapper.Map<AuthenticationResponse>(result)),
        errors => ErrorHandling.GenerateProblemDetails(ctx, errors)
       );
     });

    authRoutesGroup.MapPost("/login", async (
      HttpContext ctx,
      LoginRequest req,
      ISender mediator,
      IMapper mapper) =>
    {
      var query = mapper.Map<LoginQuery>(req);
      var authResult = await mediator.Send(query);

      return authResult.Match(
        result => Results.Ok(mapper.Map<AuthenticationResponse>(result)),
        errors => ErrorHandling.GenerateProblemDetails(ctx, errors)
      );
    });
  }

}
