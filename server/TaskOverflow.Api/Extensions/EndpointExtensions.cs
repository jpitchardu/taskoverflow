using TaskOverflow.Api.Filters;

namespace TaskOverflow.Api.Extensions;

public static class EndpointExtensions
{
  public static RouteHandlerBuilder WithValidation<T>(this RouteHandlerBuilder builder) where T : class
  {
    return builder.AddEndpointFilter<ValidationFilter<T>>();
  }
}