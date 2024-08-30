namespace Plottar.Api.Errors;

using ErrorOr;

public static class ErrorHandling
{
  public static IResult Problem(List<Error> errors)
  {
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
