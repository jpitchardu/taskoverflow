using System.ComponentModel.DataAnnotations;

namespace TaskOverflow.Api.Filters;

public class ValidationFilter<T> : IEndpointFilter where T : class
{
  public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
  {
    var argument = context.Arguments.OfType<T>().FirstOrDefault();

    if (argument is null)
    {
      return await next(context);
    }

    var validationResults = new List<ValidationResult>();
    var validationContext = new ValidationContext(argument);

    if (!Validator.TryValidateObject(argument, validationContext, validationResults, true))
    {
      var errors = validationResults
        .GroupBy(vr => vr.MemberNames.FirstOrDefault() ?? "general")
        .ToDictionary(g => g.Key, g => g.Select(vr => vr.ErrorMessage ?? "Unknown error")
        .ToArray());

      return Results.ValidationProblem(errors);
    }

    return await next(context);
  }
}