
namespace Plottar.Application.Common.Behaviors;

using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using FluentValidation;
using MediatR;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? val = null) :
  IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{

  private readonly IValidator<TRequest>? validator = val;

  public async Task<TResponse> Handle(
    TRequest request,
    RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken)
  {
    if (this.validator is null)
    {
      return await next();
    }
    var validationResult = await this.validator.ValidateAsync(request, cancellationToken);

    if (validationResult.IsValid)
    {
      return await next();
    }

    var errors = validationResult.Errors
    .ConvertAll(err => Error.Validation(err.PropertyName, err.ErrorMessage));

    return (dynamic)errors;
  }

}
