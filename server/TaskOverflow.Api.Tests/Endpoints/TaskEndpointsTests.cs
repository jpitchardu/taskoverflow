using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using TaskOverflow.Api.DTOs;
using TaskOverflow.Api.Models;

namespace TaskOverflow.Api.Tests.Endpoints;

public class TaskEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{

  private readonly WebApplicationFactory<Program> _factory;
  private readonly HttpClient _client;

  private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true
  };

  public TaskEndpointsTests(WebApplicationFactory<Program> factory)
  {
    _factory = factory.WithWebHostBuilder(builder =>
    {
      builder.ConfigureServices(builder =>
      {

      });
    });

    _client = _factory.CreateClient();
  }

  [Fact]
  public async Task GetTask_ReturnsOkResult()
  {

    var response = await _client.GetAsync("/task");

    response.EnsureSuccessStatusCode();

    var responseContent = await response.Content.ReadAsStringAsync();

    var result = JsonSerializer.Deserialize<GetTasksResponse>(responseContent, _jsonOptions);

    var expectedTask = new TodoTask("Task 1", "Description 1");

    result.Should().NotBeNull();
    result.Tasks.Should().HaveCount(1);

    result.Tasks.First().Title.Should().Be(expectedTask.Title);
    result.Tasks.First().Description.Should().Be(expectedTask.Description);
  }

  [Fact]
  public async Task CreateTask_ReturnsCreatedResult()
  {

    var todoTaskToCreate = new TodoTask("Task 1", "Description 1");

    var json = JsonSerializer.Serialize(todoTaskToCreate);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    var response = await _client.PostAsync("/task", content);

    response.EnsureSuccessStatusCode();
    response.StatusCode.Should().Be(HttpStatusCode.Created);

    var responseContent = await response.Content.ReadAsStringAsync();
    var result = JsonSerializer.Deserialize<CreateTaskResponse>(responseContent, _jsonOptions);


    result.Should().NotBeNull();
    result.Task.Title.Should().Be(todoTaskToCreate.Title);
    result.Task.Description.Should().Be(todoTaskToCreate.Description);
  }

  [Fact]
  public async Task UpdateTask_ReturnsOkResult()
  {

    var todoTaskToCreate = new TodoTask("Task 1", "Description 1");

    var json = JsonSerializer.Serialize(todoTaskToCreate);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    var createResponse = await _client.PostAsync("/task", content);
    createResponse.EnsureSuccessStatusCode();

    var createResponseContent = await createResponse.Content.ReadAsStringAsync();
    var createResult = JsonSerializer.Deserialize<CreateTaskResponse>(createResponseContent, _jsonOptions);

    createResult.Should().NotBeNull();

    var todoTaskToUpdate = new TodoTask("Task 2", "Description 2");

    var updateJson = JsonSerializer.Serialize(todoTaskToUpdate);
    var updateContent = new StringContent(updateJson, Encoding.UTF8, "application/json");

    var updateResponse = await _client.PatchAsync($"/task/{createResult.Task.Id}", updateContent);

    updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    var updateResponseContent = await updateResponse.Content.ReadAsStringAsync();
    var updateResult = JsonSerializer.Deserialize<UpdateTaskResponse>(updateResponseContent, _jsonOptions);

    updateResult.Should().NotBeNull();
    updateResult.Task.Title.Should().Be(todoTaskToUpdate.Title);
    updateResult.Task.Description.Should().Be(todoTaskToUpdate.Description);
  }

  [Fact]
  public async Task DeleteTask_ReturnsNoContentResult()
  {
    var todoTaskToCreate = new TodoTask("Task 1", "Description 1");

    var json = JsonSerializer.Serialize(todoTaskToCreate);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    var createResponse = await _client.PostAsync("/task", content);
    createResponse.EnsureSuccessStatusCode();

    var createResponseContent = await createResponse.Content.ReadAsStringAsync();
    var createResult = JsonSerializer.Deserialize<CreateTaskResponse>(createResponseContent, _jsonOptions);

    createResult.Should().NotBeNull();

    var deleteResponse = await _client.DeleteAsync($"/task/{createResult.Task.Id}");
    deleteResponse.EnsureSuccessStatusCode();
    deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
  }
}