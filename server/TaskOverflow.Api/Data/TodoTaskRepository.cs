using Microsoft.EntityFrameworkCore;
using TaskOverflow.Api.Exceptions;
using TaskOverflow.Api.Models;

namespace TaskOverflow.Api.Data;

public interface ITodoTaskRepository
{
  Task<TodoTask> CreateTask(TodoTask task, CancellationToken cancellationToken = default);
  Task<TodoTask> GetTaskById(Guid id, CancellationToken cancellationToken = default);
  Task<TodoTask> UpdateTask(Guid id, TodoTask task, CancellationToken cancellationToken = default);
  Task<TodoTask> DeleteTask(Guid id, CancellationToken cancellationToken = default);
  Task<IEnumerable<TodoTask>> GetAllTasks(CancellationToken cancellationToken = default);
}

public class TodoTaskRepository(TaskOverflowDbContext context) : ITodoTaskRepository
{
  public async Task<TodoTask> CreateTask(TodoTask task, CancellationToken cancellationToken = default)
  {
    context.Tasks.Add(task);

    await context.SaveChangesAsync(cancellationToken);

    return task;
  }

  public async Task<TodoTask> DeleteTask(Guid id, CancellationToken cancellationToken = default)
  {
    var task = await context.Tasks.FindAsync(id) ?? throw new NotFoundException();


    context.Tasks.Remove(task);

    await context.SaveChangesAsync(cancellationToken);

    return task;
  }

  public async Task<IEnumerable<TodoTask>> GetAllTasks(CancellationToken cancellationToken = default)
  {
    return await context.Tasks.ToListAsync(cancellationToken);
  }

  public async Task<TodoTask> GetTaskById(Guid id, CancellationToken cancellationToken = default)
  {
    var task = await context.Tasks.FindAsync(id, cancellationToken) ?? throw new NotFoundException();

    return task;
  }

  public async Task<TodoTask> UpdateTask(Guid id, TodoTask task, CancellationToken cancellationToken = default)
  {
    var existingTask = await context.Tasks.FindAsync(id) ?? throw new NotFoundException();

    existingTask.Title = task.Title;

    await context.SaveChangesAsync(cancellationToken);

    return existingTask;
  }
}
