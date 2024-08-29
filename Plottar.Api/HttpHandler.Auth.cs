namespace Plottar.Api.HttpHandler;

using Plottar.Application.Services;
using Plottar.Contracts.Authentication;

public static class AuthenticationHandler
{
  public static void MapAuthRoutes(this IEndpointRouteBuilder endpoints)
  {
    endpoints.MapPost("/auth/register", (RegisterRequest req, IAuthenticationService authService) =>
     {
       var authResult = authService.Register(
               req.Email,
               req.Password,
               req.FirstName,
               req.LastName);

       var response = new AuthenticationResponse(
               authResult.User.Id,
               authResult.User.FirstName,
               authResult.User.LastName,
               authResult.User.Email,
               authResult.Token
           );

       return Results.Ok(response);
     }
    );

    endpoints.MapPost("/auth/login", (LoginRequest req, IAuthenticationService authService) =>
     {
       var authResult = authService.Login(
               req.Email,
               req.Password);

       var response = new AuthenticationResponse(
               authResult.User.Id,
               authResult.User.FirstName,
               authResult.User.LastName,
               authResult.User.Email,
               authResult.Token
           );

       return Results.Ok(response);
     }
    );
  }
}
