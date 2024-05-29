using Microsoft.AspNetCore.Mvc;

namespace MinimalApi.Api.ToDos.V1.DeleteById
{
    internal static class DeleteToDoByIdEndpoint
    {
        internal static RouteHandlerBuilder MapDeleteById(this IEndpointRouteBuilder routeGroupBuilder)
        {
            return routeGroupBuilder.MapDelete("todos/{id}", (int id, [FromServices] DeleteToDoById deleteToDoById) =>
                                    {
                                        deleteToDoById.Execute(id);
                                        return Results.NoContent();
                                    }).WithSummary("Delete ToDo by Id")
                                    .Produces(202)
                                    .Produces(400, typeof(ProblemDetails))
                                    .Produces(500, typeof(ProblemDetails))
                                    // .WithName("DeleteToDoById")
                                    .WithSummary("Provide a specific ToDo by its Id")
                                    .WithTags("ToDo")
                                    .WithDescription("Test Description")
                                    .WithOpenApi()
                                    .WithMetadata(new ObsoleteAttribute("This endpoint is obsolete. Use /new-endpoint instead."))
                                    .MapToApiVersion(1);
        }
    }
}
