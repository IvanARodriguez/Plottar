namespace Plottar.Application.Common.Interfaces.Authentication;

using Plottar.Domain;

public interface IJwtGenerator
{
  string GenerateToken(User user);
}
