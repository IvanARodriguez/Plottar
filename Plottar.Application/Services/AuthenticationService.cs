namespace Plottar.Application.Services;

using System.ComponentModel.DataAnnotations;
using Plottar.Application.Common.Interfaces.Authentication;
using Plottar.Application.Common.Interfaces.Persistence;
using Plottar.Domain;

public class AuthenticationService(IJwtGenerator jwtGen, IUserRepository userRepo) : IAuthenticationService
{
  private readonly IJwtGenerator jwtGenerator = jwtGen;
  private readonly IUserRepository userRepository = userRepo;

  public AuthenticationResult Login(string email, string password)
  {
    // 1. Validate user exists
    if (this.userRepository.GetUserByEmail(email) is not User user)
    {
      throw new ArgumentException("User not found", email);
    }
    // 2. Validate Password is correct
    if (user.Password != password)
    {
      throw new ValidationException("Invalid password");
    }

    // 3. Create JWT
    var token = this.jwtGenerator.GenerateToken(user);

    return new(user, token);
  }

  public AuthenticationResult Register(string email, string password, string firstName, string lastName)
  {
    // 1. Validate User doesn't Exists
    if (this.userRepository.GetUserByEmail(email) is not null)
    {
      throw new ArgumentException("User with this email already exists!", email);
    }
    // 2. Create user (generate Unique Id), save to Db
    var user = new User
    {
      FirstName = firstName,
      LastName = lastName,
      Email = email,
      Password = password,
      Id = Guid.NewGuid(),
    };

    this.userRepository.Add(user);

    // 3. Create JWT Token
    var token = this.jwtGenerator.GenerateToken(user);

    return new(user, token);
  }
}
