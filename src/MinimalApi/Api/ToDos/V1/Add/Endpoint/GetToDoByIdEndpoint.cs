using Microsoft.AspNetCore.Mvc;
using MinimalApi.Api.ToDos.V1.GetAll;

namespace MinimalApi.Api.ToDos.V1.Add
{
    internal static class AddNewToDoEndpoint
    {
        internal static RouteHandlerBuilder MapAddNewToDo(this IEndpointRouteBuilder routeGroupBuilder)
        {
            return routeGroupBuilder.MapPost("todos", (Todo todo, [FromServices] AddNewToDo addNewToDo) =>
                                    {
                                        return Results.Ok(addNewToDo.Execute(todo));
                                    }).WithSummary("Add new todo")
                                    .Produces(200, typeof(Todo))
                                    .Produces(400, typeof(ProblemDetails))
                                    .Produces(500, typeof(ProblemDetails))
                                    .WithName("addToDoV1")
                                    .WithSummary("Provide a specific ToDo by its Id")
                                    .WithTags("ToDo")
                                    .WithDescription("Test Description")
                                    .WithOpenApi()
                                    .WithMetadata(new ObsoleteAttribute("This endpoint is obsolete. Use /new-endpoint instead."))
                                    .MapToApiVersion(1);
        }
    }
}
