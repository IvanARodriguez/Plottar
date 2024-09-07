
namespace Api.HttpHandlers;

using Api.Extensions;

public static class ErrorHandlers
{
  public static void MapErrorEndpoints(this WebApplication app)
  {

    // General error handling endpoint
    app.MapGet("/error", (HttpContext context) =>
    {
      return ProblemDetailsExtensions.CreateProblemWithErrors(
              "General Error",
              "An unexpected error occurred. Please try again later.",
              StatusCodes.Status500InternalServerError,
              context);
    });

  }

}
