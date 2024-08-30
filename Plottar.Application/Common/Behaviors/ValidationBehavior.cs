
namespace Plottar.Application.Common.Behaviors;

using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using Plottar.Application.Commands.Register;

public class ValidateRegisterCommandBehavior : IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>
{
  public async Task<ErrorOr<AuthenticationResult>> Handle(
    RegisterCommand request,
    RequestHandlerDelegate<ErrorOr<AuthenticationResult>> next,
    CancellationToken cancellationToken)
  {
    // Before the handler
    var result = await next();

    // After the handler

    return result;
  }
}
