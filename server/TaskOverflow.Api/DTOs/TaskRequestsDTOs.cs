
using System.ComponentModel.DataAnnotations;
using TaskOverflow.Api.Models;

namespace TaskOverflow.Api.DTOs;

public record CreateTaskRequest
{
  [Required(ErrorMessage = "Title is required")]
  [StringLength(20, ErrorMessage = "Title must be less than 20 characters")]
  [MinLength(1, ErrorMessage = "Title cannot be empty")]
  public string Title { get; set; } = string.Empty;
}

public record UpdateTaskRequest(bool IsCompleted);

public record GetTasksResponse(List<TodoTask> Tasks);
public record GetTaskResponse(TodoTask Task);
public record CreateTaskResponse(TodoTask Task);
public record UpdateTaskResponse(TodoTask Task);