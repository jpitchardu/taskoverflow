using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using TaskOverflow.Api.Data;
using TaskOverflow.Api.Exceptions;
using TaskOverflow.Api.Models;
using TaskOverflow.Api.Services;

namespace TaskOverflow.Api.Tests.Services;

public class TodoTaskServiceTests
{

  private readonly ITodoTaskRepository _repository;
  private readonly ILogger<TodoTaskService> _logger;
  private readonly TodoTaskService _service;

  public TodoTaskServiceTests()
  {
    _repository = Substitute.For<ITodoTaskRepository>();
    _logger = Substitute.For<ILogger<TodoTaskService>>();

    _service = new TodoTaskService(_repository, _logger);
  }

  [Fact]
  public async Task GetAllTasks_ShouldReturnAllTasks()
  {
    var tasks = new List<TodoTask>
    {
      new TodoTask("Test Task 1"),
      new TodoTask("Test Task 2"),
      new TodoTask("Test Task 3"),
    };

    _repository.GetAllTasks().Returns(Task.FromResult(tasks.AsEnumerable()));

    var result = await _service.GetAllTasks();

    result.Should().BeEquivalentTo(tasks);
  }

  [Fact]
  public async Task GetTaskById_ShouldReturnTask()
  {
    var task = new TodoTask("Test Task");

    _repository.GetTaskById(task.Id).Returns(Task.FromResult(task));

    var result = await _service.GetTaskById(task.Id);

    result.Should().BeEquivalentTo(task);
  }

  [Fact]
  public async Task GetTaskById_ShouldThrowNotFoundException_WhenTaskDoesNotExist()
  {
    var taskId = Guid.NewGuid();

    _repository.GetTaskById(taskId).Throws(new NotFoundException());

    var act = () => _service.GetTaskById(taskId);

    await act.Should().ThrowAsync<NotFoundException>().WithMessage("Not found");
  }

  [Fact]
  public async Task CreateTask_ShouldCreateTask()
  {
    var task = new TodoTask("Test Task");

    await _service.CreateTask(task);

    await _repository.Received(1).CreateTask(task);
  }


  [Fact]
  public async Task UpdateTask_ShouldUpdateTask()
  {
    var task = new TodoTask("Test Task");

    await _service.UpdateTask(task.Id, task);

    await _repository.Received(1).UpdateTask(task.Id, task);
  }

  [Fact]
  public async Task UpdateTask_ShouldThrowNotFoundException_WhenTaskDoesNotExist()
  {
    var taskId = Guid.NewGuid();

    _repository.UpdateTask(taskId, Arg.Any<TodoTask>()).Throws(new NotFoundException());

    var act = () => _service.UpdateTask(taskId, new TodoTask("Test Task"));

    await act.Should().ThrowAsync<NotFoundException>().WithMessage("Not found");
  }

  [Fact]
  public async Task DeleteTask_ShouldDeleteTask()
  {
    var taskId = Guid.NewGuid();

    await _service.DeleteTask(taskId);

    await _repository.Received(1).DeleteTask(taskId);
  }

  [Fact]
  public async Task DeleteTask_ShouldThrowNotFoundException_WhenTaskDoesNotExist()
  {
    var taskId = Guid.NewGuid();

    _repository.DeleteTask(taskId).Throws(new NotFoundException());

    var act = () => _service.DeleteTask(taskId);

    await act.Should().ThrowAsync<NotFoundException>().WithMessage("Not found");
  }

}