
namespace Plottar.Api.Errors;

using System.Diagnostics;
using ErrorOr;

public static class CustomProblemDetails
{
  public static void Customize(ProblemDetailsContext context)
  {
    var errors = context.HttpContext.Items["errors"] as List<Error>;
    if (errors is not null && errors.Count != 0)
    {
      context.ProblemDetails.Extensions.Add(
        "errorsCode",
        errors.Select(e => $"{e.Code}: {e.Description}"));
    }
    context.ProblemDetails.Extensions.Add("traceId", Activity.Current?.TraceId.ToString());
  }
}
