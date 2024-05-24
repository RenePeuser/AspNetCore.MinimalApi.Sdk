using MinimalApi.Api.ToDos;
using MinimalApi.Extensionmethods;

var builder = WebApplication.CreateBuilder(args);

// Brand new cool stuff -> Ready to run registration
builder.Services.AddSimpleMinimalApiEnvironment();

builder.Services.AddToDos();

var app = builder.Build();

app.UseSimpleMinimalApiEnvironment(endpoint =>
{
    // API endpoint registrations
    // API -> Domain -> V1 -> GetAll
    endpoint.UseToDos();
});

app.Run();