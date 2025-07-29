using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute.ExceptionExtensions;
using TaskOverflow.Api.Data;
using TaskOverflow.Api.Exceptions;
using TaskOverflow.Api.Models;

namespace TaskOverflow.Api.Tests.Data;

public class TodoTaskRepositoryTests : IDisposable
{

  private readonly TaskOverflowDbContext _context;
  private readonly TodoTaskRepository _repository;


  public TodoTaskRepositoryTests()
  {
    var options = new DbContextOptionsBuilder<TaskOverflowDbContext>()
      .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
      .Options;

    _context = new TaskOverflowDbContext(options);
    _repository = new TodoTaskRepository(_context);
  }

  [Fact]
  public async Task CreateTask_ShouldCreateTask()
  {

    var task = new TodoTask("Test Task", "Test Description");
    await _repository.CreateTask(task);

    var createdTask = await _context.Tasks.FindAsync(task.Id);

    createdTask.Should().NotBeNull();
    createdTask.Title.Should().Be(task.Title);
    createdTask.Description.Should().Be(task.Description);
  }


  [Fact]
  public async Task GetTaskById_ShouldGetTaskById()
  {
    var taskToCreate = new TodoTask("Test Task", "Test Description");

    _context.Tasks.Add(taskToCreate);
    await _context.SaveChangesAsync();

    var createdTask = await _repository.GetTaskById(taskToCreate.Id);

    createdTask.Should().NotBeNull();
    createdTask.Title.Should().Be(taskToCreate.Title);
    createdTask.Description.Should().Be(taskToCreate.Description);
  }

  [Fact]
  public async Task GetTaskById_ShouldThrowNotFoundException_WhenTaskDoesNotExist()
  {
    var taskId = Guid.NewGuid();

    var act = () => _repository.GetTaskById(taskId);

    await act.Should().ThrowAsync<NotFoundException>().WithMessage("Not found");

  }

  [Fact]
  public async Task GetAllTasks_ShouldGetAllTasks()
  {
    var tasksToCreate = new List<TodoTask>
    {
      new TodoTask("Test Task 1", "Test Description 1"),
      new TodoTask("Test Task 2", "Test Description 2"),
      new TodoTask("Test Task 3", "Test Description 3"),
    };

    _context.Tasks.AddRange(tasksToCreate);
    await _context.SaveChangesAsync();

    var tasks = await _repository.GetAllTasks();

    tasks.Should().HaveCount(3);
    tasks.Should().BeEquivalentTo(tasksToCreate);
  }

  [Fact]
  public async Task UpdateTask_ShouldUpdateTask()
  {
    var taskToCreate = new TodoTask("Test Task", "Test Description");

    _context.Tasks.Add(taskToCreate);
    await _context.SaveChangesAsync();

    var updatedTask = new TodoTask("Updated Task", "Updated Description");

    await _repository.UpdateTask(taskToCreate.Id, updatedTask);

    var createdTask = await _context.Tasks.FindAsync(taskToCreate.Id);

    createdTask.Should().NotBeNull();
    createdTask.Title.Should().Be(updatedTask.Title);
    createdTask.Description.Should().Be(updatedTask.Description);
  }

  [Fact]
  public async Task UpdateTask_ShouldThrowNotFoundException_WhenTaskDoesNotExist()
  {
    var taskId = Guid.NewGuid();

    var act = () => _repository.UpdateTask(taskId, new TodoTask("Test Task", "Test Description"));

    await act.Should().ThrowAsync<NotFoundException>().WithMessage("Not found");
  }

  [Fact]
  public async Task DeleteTask_ShouldDeleteTask()
  {
    var taskToCreate = new TodoTask("Test Task", "Test Description");

    _context.Tasks.Add(taskToCreate);
    await _context.SaveChangesAsync();

    await _repository.DeleteTask(taskToCreate.Id);

    var createdTask = await _context.Tasks.FindAsync(taskToCreate.Id);

    createdTask.Should().BeNull();
  }

  [Fact]
  public async Task DeleteTask_ShouldThrowNotFoundException_WhenTaskDoesNotExist()
  {
    var taskId = Guid.NewGuid();

    var act = () => _repository.DeleteTask(taskId);

    await act.Should().ThrowAsync<NotFoundException>().WithMessage("Not found");
  }

  public void Dispose()
  {
    _context.Database.EnsureDeleted();
    _context.Dispose();
  }
}