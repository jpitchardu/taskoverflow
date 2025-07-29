namespace TaskOverflow.Api.Models;

public class TodoTask
{


  public Guid Id { get; set; } = Guid.NewGuid();
  public string Title { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


  public TodoTask(string title)
  {
    Title = title;
  }

  private TodoTask() { }

}