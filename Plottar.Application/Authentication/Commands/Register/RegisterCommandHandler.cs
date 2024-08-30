namespace Plottar.Application.Authentication.Commands.Register;

using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using Plottar.Application.Commands.Register;
using Plottar.Application.Common;
using Plottar.Application.Common.Interfaces.Authentication;
using Plottar.Application.Common.Interfaces.Persistence;
using Plottar.Domain;
using Plottar.Domain.Common.Errors;

public class RegisterCommandHandler(IJwtGenerator jwtGen, IUserRepository userRepo) :
  IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
  private readonly IJwtGenerator jwtGenerator = jwtGen;
  private readonly IUserRepository userRepository = userRepo;
  public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
  {
    // 1. Validate User doesn't Exists
    if (this.userRepository.GetUserByEmail(command.Email) is not null)
    {
      return Errors.User.DuplicateEmail;
    }
    // 2. Create user (generate Unique Id), save to Db
    var user = new User
    {
      FirstName = command.FirstName,
      LastName = command.LastName,
      Email = command.Email,
      Password = command.Password,
      Id = Guid.NewGuid(),
    };

    this.userRepository.Add(user);

    // 3. Create JWT Token
    var token = this.jwtGenerator.GenerateToken(user);

    return new AuthenticationResult(user, token);
  }
}
