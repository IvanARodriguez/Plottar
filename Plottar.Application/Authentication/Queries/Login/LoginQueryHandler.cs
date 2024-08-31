namespace Plottar.Application.Authentication.Query.Login;

using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using Plottar.Application.Common;
using Plottar.Application.Common.Interfaces.Authentication;
using Plottar.Application.Common.Interfaces.Persistence;
using Plottar.Application.Queries.Login;
using Plottar.Domain;
using Plottar.Domain.Common.Errors;

public class LoginQueryHandler(IJwtGenerator jwtGen, IUserRepository userRepo) :
  IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
  private readonly IJwtGenerator jwtGenerator = jwtGen;
  private readonly IUserRepository userRepository = userRepo;
  public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;
    // 1. Validate user exists
    if (this.userRepository.GetUserByEmail(request.Email) is not User user)
    {
      return Errors.Authentication.InvalidCredentials;
    }
    // 2. Validate Password is correct
    if (user.Password != request.Password)
    {
      return new[] { Errors.Authentication.InvalidCredentials };
    }

    // 3. Create JWT
    var token = this.jwtGenerator.GenerateToken(user);

    return new AuthenticationResult(user, token);
  }
}
