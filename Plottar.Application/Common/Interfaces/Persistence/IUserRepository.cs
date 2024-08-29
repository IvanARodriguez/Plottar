namespace Plottar.Application.Common.Interfaces.Persistence;

using Plottar.Domain;

public interface IUserRepository
{
  User? GetUserByEmail(string email);
  void Add(User user);
}
