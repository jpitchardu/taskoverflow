using TaskOverflow.Api.DTOs;
using TaskOverflow.Api.Exceptions;
using TaskOverflow.Api.Models;
using TaskOverflow.Api.Services;

namespace TaskOverflow.Api.Endpoints;


public static class TaskEndpoints
{
  public static void MapTaskEndpoints(this IEndpointRouteBuilder app)
  {
    app.MapGet("/task", async (ITodoTaskService todoTaskService, CancellationToken cancellationToken) =>
    {
      try
      {
        var tasks = await todoTaskService.GetAllTasks(cancellationToken);

        return Results.Ok(new GetTasksResponse([.. tasks]));
      }
      catch (Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    });

    app.MapGet("/task/{id}", async (Guid id, ITodoTaskService todoTaskService, CancellationToken cancellationToken) =>
    {
      try
      {
        var task = await todoTaskService.GetTaskById(id, cancellationToken);

        return Results.Ok(new GetTaskResponse(task));
      }
      catch (Exception ex)
      {

        if (ex is NotFoundException)
        {
          return Results.NotFound(ex.Message);
        }

        return Results.Problem(ex.Message);
      }
    });

    app.MapPost("/task", async (CreateTaskRequest request, ITodoTaskService todoTaskService, CancellationToken cancellationToken) =>
    {
      try
      {
        var task = new TodoTask(request.Title, request.Description);

        var createdTask = await todoTaskService.CreateTask(task, cancellationToken);

        return Results.Created(
          $"/task/{createdTask.Id}",
          new CreateTaskResponse(createdTask)
        );
      }
      catch (Exception ex)
      {
        return Results.Problem(ex.Message);
      }
    });

    app.MapPatch("/task/{id}", async (Guid id, UpdateTaskRequest request, ITodoTaskService todoTaskService, CancellationToken cancellationToken) =>
    {
      try
      {
        var task = new TodoTask(request.Title, request.Description);

        var updatedTask = await todoTaskService.UpdateTask(id, task, cancellationToken);

        return Results.Ok(new UpdateTaskResponse(updatedTask));
      }
      catch (Exception ex)
      {
        if (ex is NotFoundException)
        {
          return Results.NotFound(ex.Message);
        }

        return Results.Problem(ex.Message);
      }
    });

    app.MapDelete("/task/{id}", async (Guid id, ITodoTaskService todoTaskService, CancellationToken cancellationToken) =>
    {
      try
      {
        await todoTaskService.DeleteTask(id, cancellationToken);

        return Results.NoContent();
      }
      catch (Exception ex)
      {
        if (ex is NotFoundException)
        {
          return Results.NotFound(ex.Message);
        }

        return Results.Problem(ex.Message);
      }
    });
  }
} 
