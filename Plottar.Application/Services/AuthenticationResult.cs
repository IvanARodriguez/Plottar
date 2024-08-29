namespace Plottar.Application.Services;

using Plottar.Domain;

public record AuthenticationResult(
 User User,
  string Token
);
