using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Plottar_API.Middlewares
{
  public class GlobalExceptionHandlerMiddleware: IMiddleware
  {
    private readonly ILogger _logger;
        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
          _logger = logger;
        }

    public async Task InvokeAsync(HttpContext ctx, RequestDelegate next)
    {
      try
      {
        await next(ctx);
      }
      catch (Exception exn)
      {
        _logger.LogError(exn, exn.Message);

        ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        ProblemDetails problem = new ProblemDetails()
        {
          Status = (int)HttpStatusCode.InternalServerError,
          Type = "Server error",
          Title = "Server error",
          Detail = "An internal server error has ocurred"
        };

        var json = JsonSerializer.Serialize(problem);

        await ctx.Response.WriteAsync(json);

        ctx.Response.ContentType = "application/json";
      }
    }
  }
}
