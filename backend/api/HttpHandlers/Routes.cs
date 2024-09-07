namespace Api.HttpHandlers;

public static class Routes
{
  public static void MapApiRoutes(this WebApplication app)
  {
    app.UsePathBase("/api");
    app.MapJobEndpoints();
    app.MapErrorEndpoints();
  }
}
