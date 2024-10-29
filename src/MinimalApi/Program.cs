using MinimalApi.Api.ToDos;
using MinimalApi.Api.ToDos.V1.DbContext;
using MinimalApi.Api.ToDos.V1.GetAll;
using MinimalApi.ErrorHandling;
using MinimalApi.Extensionmethods;

var builder = WebApplication.CreateBuilder(args);

// Brand new cool stuff -> Ready to run registration
builder.Services.AddSimpleMinimalApiEnvironment();

builder.Services.AddErrorHandlingMiddleware();

builder.Services.AddToDos();

var app = builder.Build();
app.UseSimpleMinimalApiEnvironment(endpoint =>
{
    // API endpoint registrations
    // API -> Domain -> V1 -> GetAll
    endpoint.UseToDos();
});

app.UseErrorHandling();

app.Services.GetRequiredService<TodoRepository>().Add(new()
{
    Notes = new Notes
    {
        Hint = "Cool stuff",
        Key = "CS"
    },
    DueBy = DateOnly.FromDateTime(DateTime.Now),
    Id = 1,
    IsComplete = true,
    Title = "Walk with the dog"
});

app.Run();

// important for api tests !
public partial class Program
{
}
