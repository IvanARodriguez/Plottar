namespace Plottar.Application.Services;

using Plottar.Application.Common.Interfaces.Authentication;

public class AuthenticationService(IJwtGenerator jwtGenerator) : IAuthenticationService
{

  public AuthenticationResult Login(string email, string password)
  {
    return new(Guid.NewGuid(), "firstName", "lastName", email, "token");
  }

  public AuthenticationResult Register(string email, string password, string firstName, string lastName)
  {
    //  Check if the user exist

    //  Query the user (generate unique Id)

    // create JWT token
    var userId = Guid.NewGuid();

    var token = jwtGenerator.GenerateToken(userId, firstName, lastName);

    return new(Guid.NewGuid(), firstName, lastName, email, token);
  }
}
