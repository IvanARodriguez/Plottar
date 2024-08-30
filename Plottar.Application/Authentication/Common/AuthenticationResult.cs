namespace Plottar.Application.Common;

using Plottar.Domain;

public record AuthenticationResult(
  User User,
  string Token
);
