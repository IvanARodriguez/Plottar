namespace Api.Extensions;

using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public static class ProblemDetailsExtensions
{
  public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services)
  {
    services.AddProblemDetails(options => options.CustomizeProblemDetails = ctx =>
      {
        var errorsExists = ctx.ProblemDetails.Extensions.ContainsKey("errors");
        if (errorsExists)
        {
          ctx.ProblemDetails.Extensions.Add("errors", new Dictionary<string, string[]>());
        }
        var traceId = Activity.Current?.Id ?? ctx.HttpContext.TraceIdentifier;
        ctx.ProblemDetails.Extensions.Add("traceId", traceId);
      });

    return services;
  }

  public static IResult CreateProblemWithErrors(
        string title,
        string detail,
        int statusCode,
        HttpContext context,
        IDictionary<string, string[]>? errors = null)
  {
    var problemDetails = new ProblemDetails
    {
      Title = title,
      Detail = detail,
      Status = statusCode,
      Instance = context.Request.Path
    };

    var exceptionHandlerFeatures = context.Features.Get<IExceptionHandlerFeature>();
    var exception = exceptionHandlerFeatures?.Error;

    if (exception is UnauthorizedAccessException)
    {
      problemDetails.Status = StatusCodes.Status401Unauthorized;
      problemDetails.Title = "Unauthorized access";
    }

    // Add errors if provided
    if (errors != null && errors.Any())
    {
      problemDetails.Extensions.Add("errors", errors);
    }
    var traceId = Activity.Current?.Id ?? context.TraceIdentifier;
    // Include traceId for troubleshooting purposes
    problemDetails.Extensions.Add("traceId", traceId);

    return Results.Problem(problemDetails.Detail, problemDetails.Instance, problemDetails.Status, problemDetails.Title);
  }


}
