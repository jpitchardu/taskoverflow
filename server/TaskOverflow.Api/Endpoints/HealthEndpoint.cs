namespace TaskOverflow.Api.Endpoints;

public static class HealthEndpoint
{
  public static void MapHealthEndpoint(this IEndpointRouteBuilder app)
  {
    app.MapGet("/health", () => "OK");
  }
}