namespace Plottar.Application.Services;
public class AuthenticationService : IAuthenticationService
{
  public AuthenticationResult Login(string email, string password) => new(Guid.NewGuid(), "firstName", "lastName", email, "token");

  public AuthenticationResult Register(string email, string password, string firstName, string lastName) => new(Guid.NewGuid(), firstName, password, email, "token");
}
