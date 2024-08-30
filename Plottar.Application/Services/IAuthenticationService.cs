namespace Plottar.Application.Services;

using ErrorOr;

public interface IAuthenticationService
{
  ErrorOr<AuthenticationResult> Register(string email, string password, string firstName, string lastName);
  ErrorOr<AuthenticationResult> Login(string email, string password);
}
