namespace Plottar.Api.Errors;

using ErrorOr;

public static class ErrorHandling
{
  public static IResult GenerateProblemDetails(HttpContext ctx, List<Error> errors)
  {
    if (errors.Count == 0)
    {
      return Results.Problem();
    }

    ctx.Items["errors"] = errors;

    var firstError = errors[0];

    return Problem(firstError);
  }

  private static IResult Problem(Error error)
  {
    var statusCode = error.Type switch
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

    return Results.Problem(detail: error.Description, statusCode: statusCode);
  }
}
