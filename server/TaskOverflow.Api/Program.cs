using Microsoft.EntityFrameworkCore;
using TaskOverflow.Api.Data;
using TaskOverflow.Api.Endpoints;
using TaskOverflow.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TaskOverflowDbContext>(options =>

{

    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

    if (env == "Testing")
    {
        options.UseInMemoryDatabase("TaskOverflowDb");
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
        return;
    }

    if (env == "Development")
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Default connection string not found");

    options.UseNpgsql(connectionString);

});

builder.Services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();
builder.Services.AddScoped<ITodoTaskService, TodoTaskService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapTaskEndpoints();

app.Run();

public partial class Program { }