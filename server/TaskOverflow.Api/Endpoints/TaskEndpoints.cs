using TaskOverflow.Api.DTOs;
using TaskOverflow.Api.Models;

namespace TaskOverflow.Api.Endpoints;


public static class TaskEndpoints
{
  public static void MapTaskEndpoints(this IEndpointRouteBuilder app)
  {
    app.MapGet("/task", () =>
    {
      return Results.Ok(new GetTasksResponse([new TodoTask("Task 1", "Description 1")]));
    });

    app.MapPost("/task", (CreateTaskRequest request) =>
    {

      var task = new TodoTask(request.Title, request.Description);

      return Results.Created(
        $"/task/{task.Id}",
        new CreateTaskResponse(task)
      );
    });

    app.MapPatch("/task/{id}", (Guid id, UpdateTaskRequest request) =>
    {
      var task = new TodoTask(request.Title, request.Description);

      return Results.Ok(new UpdateTaskResponse(task));
    });

    app.MapDelete("/task/{id}", (Guid id) =>
    {
      return Results.NoContent();
    });
  }
}