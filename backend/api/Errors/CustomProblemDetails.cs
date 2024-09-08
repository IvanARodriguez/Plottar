namespace Api.Errors;

using System.Diagnostics;
using ErrorOr;

public static class CustomProblemDetails
{
  public static void Customize(ProblemDetailsContext context)
  {
    var errors = context.HttpContext.Items["errors"] as List<Error>;

    if (errors is not null && errors.Count != 0)
    {
      var errorDictionary = errors.ToDictionary(
                    e => e.Code,
                    e => e.Description
                );
      context.ProblemDetails.Extensions.Add("errorsCode", errorDictionary);
    }

    context.ProblemDetails.Extensions.Add("traceId", Activity.Current?.TraceId.ToString());

    context.ProblemDetails.Extensions.Remove("exception");
  }

}
