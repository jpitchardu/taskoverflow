using TaskOverflow.Api.Data;
using TaskOverflow.Api.Exceptions;
using TaskOverflow.Api.Models;

namespace TaskOverflow.Api.Services;


public interface ITodoTaskService
{
  Task<TodoTask> CreateTask(TodoTask task, CancellationToken cancellationToken = default);
  Task<TodoTask> GetTaskById(Guid id, CancellationToken cancellationToken = default);
  Task<TodoTask> UpdateTask(Guid id, TodoTask task, CancellationToken cancellationToken = default);
  Task<TodoTask> DeleteTask(Guid id, CancellationToken cancellationToken = default);
  Task<IEnumerable<TodoTask>> GetAllTasks(CancellationToken cancellationToken = default);
}

public class TodoTaskService(ITodoTaskRepository todoTaskRepository, ILogger<TodoTaskService> logger) : ITodoTaskService
{
  public async Task<TodoTask> CreateTask(TodoTask task, CancellationToken cancellationToken = default)
  {
    logger.LogInformation("Creating task: {TaskId}", task.Id);
    return await todoTaskRepository.CreateTask(task, cancellationToken);
  }

  public async Task<TodoTask> DeleteTask(Guid id, CancellationToken cancellationToken = default)
  {
    logger.LogInformation("Deleting task: {Id}", id);
    try
    {
      return await todoTaskRepository.DeleteTask(id, cancellationToken);

    }
    catch (Exception ex)
    {
      if (ex is NotFoundException)
      {
        logger.LogWarning("Task not found: {Id}", id);
        throw;
      }
      logger.LogError(ex, "Error deleting task: {Id}", id);
      throw;
    }
  }

  public async Task<IEnumerable<TodoTask>> GetAllTasks(CancellationToken cancellationToken = default)
  {
    logger.LogInformation("Getting all tasks");
    return await todoTaskRepository.GetAllTasks(cancellationToken);
  }

  public async Task<TodoTask> GetTaskById(Guid id, CancellationToken cancellationToken = default)
  {
    logger.LogInformation("Getting task by id: {Id}", id);
    try
    {
      return await todoTaskRepository.GetTaskById(id, cancellationToken);
    }
    catch (Exception ex)
    {
      if (ex is NotFoundException)
      {
        logger.LogWarning("Task not found: {Id}", id);
        throw;
      }

      logger.LogError(ex, "Error getting task by id: {Id}", id);
      throw;
    }
  }

  public async Task<TodoTask> UpdateTask(Guid id, TodoTask task, CancellationToken cancellationToken = default)
  {
    logger.LogInformation("Updating task: {Id}", id);
    try
    {
      return await todoTaskRepository.UpdateTask(id, task, cancellationToken);
    }
    catch (Exception ex)
    {
      if (ex is NotFoundException)
      {
        logger.LogWarning("Task not found: {Id}", id);
        throw;
      }

      logger.LogError(ex, "Error updating task: {Id}", id);
      throw;
    }
  }
}