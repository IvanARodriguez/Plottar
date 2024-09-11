

namespace Api.Filters;

using System.Threading.Tasks;
using FluentValidation;

public class ValidationFilter<TRequest>(IValidator<TRequest> validator) : IEndpointFilter
{
  private readonly IValidator<TRequest> _validator = validator;
  public async ValueTask<object?> InvokeAsync(
    EndpointFilterInvocationContext context,
    EndpointFilterDelegate next)
  {
    var request = context.Arguments.OfType<TRequest>().First();

    var result = await _validator.ValidateAsync(request, context.HttpContext.RequestAborted);

    if (!result.IsValid)
    {
      return TypedResults.ValidationProblem(result.ToDictionary());
    }

    return await next(context);
  }

}

public static class ValidationExtensions
{
  public static void WithRequestValidation<TRequest>(this RouteHandlerBuilder builder)
  {
    builder.AddEndpointFilter<ValidationFilter<TRequest>>()
    .ProducesValidationProblem();
  }
}
