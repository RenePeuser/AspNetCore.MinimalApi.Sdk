using Microsoft.AspNetCore.Mvc;

namespace MinimalApi.Api.ToDos.V2.GetAll
{
    public static class GetAllToDoEndpoints
    {
        public static RouteHandlerBuilder MapGetAll(this IEndpointRouteBuilder routeGroupBuilder)
        {
            return routeGroupBuilder.MapGet("todos", ([FromServices] GetAllToDosQuery getAllToDosQuery) =>
            {
                return Results.Ok(getAllToDosQuery.Execute());
            }).WithSummary("Get all ToDos")
            .Produces(200, typeof(Todo))
            .Produces(400, typeof(ProblemDetails))
            .Produces(500, typeof(ProblemDetails))
            // .WithName("getAllToDos")
            .WithSummary("Returns all available ToDos")
            .WithTags("ToDo")
            .WithDescription("Test Description")
            .WithOpenApi()
            .MapToApiVersion(2);
        }
    }
}