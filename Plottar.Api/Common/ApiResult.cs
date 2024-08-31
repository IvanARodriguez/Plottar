namespace Plottar.Api;

using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public static class ApiResult
{
  public static IResult From<T>(ErrorOr<T> result)
  {
    return result.Match(
        success => Results.Ok(success),
        Problem
    );
  }

  public static IResult Problem(List<Error> errors)
  {
    // Here, you can customize the problem details response as per your requirements
    var firstError = errors.First();

    var problemDetails = new ProblemDetails
    {
      Status = MapStatusCode(firstError.Type),
      Title = firstError.Description,
      Detail = firstError.Code
    };

    return Results.Problem(problemDetails);
  }

  private static int MapStatusCode(ErrorType errorType)
  {
    return errorType switch
    {
      ErrorType.Conflict => StatusCodes.Status409Conflict,
      ErrorType.Validation => StatusCodes.Status400BadRequest,
      ErrorType.NotFound => StatusCodes.Status404NotFound,
      _ => StatusCodes.Status500InternalServerError,
    };
  }
}
