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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<TaskOverflowDbContext>();
    dbContext.Database.Migrate();
}

;

app.UseCors();

app.MapTaskEndpoints();
app.MapHealthEndpoint();

app.Run();

public partial class Program { }