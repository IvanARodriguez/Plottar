namespace Api.HttpHandlers;

public static class Routes
{
  public static void MapApiRoutes(this WebApplication app)
  {
    app.UsePathBase("/api");
    app.MapJobEndpoints();
    app.MapSkillsEndpoints();
    app.MapGet("/error", (HttpContext ctx) => ErrorHandlers.GenerateProblemDetails(ctx, null));
  }
}
