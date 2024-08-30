namespace Plottar.Api.HttpHandler;

using Plottar.Api.Errors;
using Plottar.Application.Services;
using Plottar.Contracts.Authentication;

public static class AuthenticationHandler
{
  public static void MapAuthRoutes(this IEndpointRouteBuilder endpoints)
  {
    endpoints.MapPost("/auth/register", (RegisterRequest req, IAuthenticationService authService, HttpContext ctx) =>
     {
       var authResult = authService.Register(
               req.Email,
               req.Password,
               req.FirstName,
               req.LastName);

       return authResult.Match(
        result => Results.Ok(MapResult(result)),
        errors => ErrorHandling.Problem(errors)
       );
     });

    endpoints.MapPost("/auth/login", (LoginRequest req, IAuthenticationService authService) =>
     {
       var authResult = authService.Login(
               req.Email,
               req.Password);

       return authResult.Match(
        result => Results.Ok(MapResult(result)),
        errors => ErrorHandling.Problem(errors)
       );
     }
    );

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
