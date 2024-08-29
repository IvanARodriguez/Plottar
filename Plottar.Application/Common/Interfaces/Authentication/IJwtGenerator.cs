namespace Plottar.Application.Common.Interfaces.Authentication;

public interface IJwtGenerator
{
  string GenerateToken(Guid userId, string firstName, string lastName);
}
