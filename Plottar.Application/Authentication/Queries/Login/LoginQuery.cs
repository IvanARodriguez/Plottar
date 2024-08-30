namespace Plottar.Application.Queries.Login;

using ErrorOr;
using MediatR;
using Plottar.Application.Common;

public record LoginQuery(
  string Email,
  string Password) : IRequest<ErrorOr<AuthenticationResult>>;
