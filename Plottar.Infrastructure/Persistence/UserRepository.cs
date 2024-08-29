namespace Plottar.Infrastructure.Persistence;

using Plottar.Application.Common.Interfaces.Persistence;
using Plottar.Domain;

public class UserRepository : IUserRepository
{
  private static readonly List<User> Users = [];
  public void Add(User user)
  {
    Users.Add(user);
  }

  public User? GetUserByEmail(string email)
  {
    return Users.SingleOrDefault(u => u.Email == email);
  }
}
