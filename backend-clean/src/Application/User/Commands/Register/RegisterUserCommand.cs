
namespace Application.User.Commands.Register;

using MediatR;

public class RegisterUserCommand(string email, string password) : IRequest<Guid>
{
  public string Email { get; } = email;
  public string Password { get; } = password;
}
