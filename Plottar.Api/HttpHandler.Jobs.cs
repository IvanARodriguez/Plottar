namespace Plottar.Api.HttpHandler;

public static class JobListingHttpHandler
{
  public static void MapJobsRoutes(this IEndpointRouteBuilder endpoint)
  {
    endpoint.MapGet("/jobs", (HttpContext ctx) =>
    {
      return Results.Ok(Array.Empty<string>());
    });
  }
}
