using MinimalApi.Api.ToDos;
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

app.Run();

// important for api tests !
public partial class Program
{
}
