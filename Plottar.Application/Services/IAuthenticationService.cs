namespace Plottar.Application.Services;
public interface IAuthenticationService
{
  AuthenticationResult Register(string email, string password, string firstName, string lastName);
  AuthenticationResult Login(string email, string password);
}
