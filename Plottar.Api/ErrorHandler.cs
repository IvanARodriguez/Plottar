namespace Plottar.Api.Errors;

using ErrorOr;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public static class ErrorHandling
{
  public static IResult GenerateProblemDetails(HttpContext ctx, List<Error> errors)
  {

    ctx.Items["errors"] = errors;

    var firstError = errors[0];

    var statusCode = firstError.Type switch
    {
      ErrorType.Conflict => StatusCodes.Status409Conflict,
      ErrorType.Validation => StatusCodes.Status400BadRequest,
      ErrorType.NotFound => StatusCodes.Status404NotFound,
      ErrorType.Failure => StatusCodes.Status500InternalServerError,
      ErrorType.Unexpected => StatusCodes.Status500InternalServerError,
      ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
      ErrorType.Forbidden => StatusCodes.Status403Forbidden,
      _ => StatusCodes.Status500InternalServerError
    };

    return Results.Problem(detail: firstError.Description, statusCode: statusCode);
  }
}
