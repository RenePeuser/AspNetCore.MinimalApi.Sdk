using Microsoft.AspNetCore.Mvc;
using MinimalApi.Api.ToDos.V1.GetAll;

namespace MinimalApi.Api.ToDos.V1.Update
{
    internal static class UpdateToDoEndpoint
    {
        internal static RouteHandlerBuilder MapUpdateToDo(this IEndpointRouteBuilder routeGroupBuilder)
        {
            return routeGroupBuilder.MapPut("todos/{id}", (int id, Todo todo, [FromServices] UpdateToDo updateToDo) =>
                                    {
                                        return Results.Ok((object?)updateToDo.Execute(id, todo));
                                    }).WithSummary("Get ToDo by Id")
                                    .Produces(200, typeof(Todo))
                                    .Produces(400, typeof(ProblemDetails))
                                    .Produces(500, typeof(ProblemDetails))
                                    .WithName("updateToDoByIdV1")
                                    .WithSummary("Provide a specific ToDo by its Id")
                                    .WithTags("ToDo")
                                    .WithDescription("Test Description")
                                    .WithOpenApi()
                                    .WithMetadata(new ObsoleteAttribute("This endpoint is obsolete. Use /new-endpoint instead."))
                                    .MapToApiVersion(1);
        }
    }
}
