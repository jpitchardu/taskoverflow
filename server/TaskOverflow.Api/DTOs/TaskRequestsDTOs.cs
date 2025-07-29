
using TaskOverflow.Api.Models;

namespace TaskOverflow.Api.DTOs;

public record CreateTaskRequest(string Title);
public record UpdateTaskRequest(bool IsCompleted);

public record GetTasksResponse(List<TodoTask> Tasks);
public record GetTaskResponse(TodoTask Task);
public record CreateTaskResponse(TodoTask Task);
public record UpdateTaskResponse(TodoTask Task);