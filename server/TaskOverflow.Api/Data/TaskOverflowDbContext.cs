using Microsoft.EntityFrameworkCore;
using TaskOverflow.Api.Models;

namespace TaskOverflow.Api.Data;

public class TaskOverflowDbContext(DbContextOptions<TaskOverflowDbContext> options) : DbContext(options)
{
  public DbSet<TodoTask> Tasks { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<TodoTask>().HasKey(t => t.Id);
    modelBuilder.Entity<TodoTask>().Property(t => t.Title).IsRequired();
    modelBuilder.Entity<TodoTask>().Property(t => t.IsCompleted).IsRequired().HasDefaultValue(false);

    modelBuilder.Entity<TodoTask>().HasIndex(t => t.UpdatedAt);
    modelBuilder.Entity<TodoTask>().HasIndex(t => t.IsCompleted);

    modelBuilder.Entity<TodoTask>().Property(t => t.CreatedAt);
    modelBuilder.Entity<TodoTask>().Property(t => t.UpdatedAt).ValueGeneratedOnUpdate();
  }
}