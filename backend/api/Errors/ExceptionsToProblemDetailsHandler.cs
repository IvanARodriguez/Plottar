

namespace Api.Errors;

using System.Net;
using Microsoft.AspNetCore.Diagnostics;

public class ExceptionsToProblemDetailsHandler(IProblemDetailsService problemService) : IExceptionHandler
{
  private readonly IProblemDetailsService _problemDetailsService = problemService;
  public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
  {
    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
    {
      HttpContext = httpContext,
      ProblemDetails =
                {
                    Title = "An error occurred",
                    Detail = exception.Message,
                    Type = exception.GetType().Name,
                },
      Exception = exception
    });
  }
}
