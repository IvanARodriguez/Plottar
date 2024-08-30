namespace Plottar.Application.Commands.Register;

using ErrorOr;
using MediatR;
using Plottar.Application.Common;

public record RegisterCommand(string FirstName,
  string LastName,
  string Email,
  string Password) : IRequest<ErrorOr<AuthenticationResult>>;
