
using TaskOverflow.Api.Models;

namespace TaskOverflow.Api.DTOs;

public record CreateTaskRequest(string Title, string Description);
public record UpdateTaskRequest(string Title, string Description);

public record GetTasksResponse(List<TodoTask> Tasks);
public record GetTaskResponse(TodoTask Task);
public record CreateTaskResponse(TodoTask Task);
public record UpdateTaskResponse(TodoTask Task);