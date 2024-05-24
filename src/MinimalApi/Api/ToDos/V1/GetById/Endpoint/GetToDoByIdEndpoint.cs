using Microsoft.AspNetCore.Mvc;
using MinimalApi.Api.ToDos.V1.GetAll;

namespace MinimalApi.Api.ToDos.V1.GetById
{
    internal static class GetToDoByIdEndpoint
    {
        internal static RouteHandlerBuilder MapGetById(this IEndpointRouteBuilder routeGroupBuilder)
        {
            return routeGroupBuilder.MapGet("todos/{id}", (int id, [FromServices] GetToDoById getToDoById) =>
                                    {
                                        var result = getToDoById.Execute(id);

                                        if (result is null)
                                        {
                                            return Results.NotFound();
                                        }

                                        return Results.Ok(result);
                                    }).WithSummary("Get ToDo by Id")
                                    .Produces(200, typeof(Todo))
                                    .Produces(400, typeof(ProblemDetails))
                                    .Produces(500, typeof(ProblemDetails))
                                    // .WithName("getToDoById")
                                    .WithSummary("Provide a specific ToDo by its Id")
                                    .WithTags("ToDo")
                                    .WithDescription("Test Description")
                                    .WithOpenApi()
                                    .MapToApiVersion(1);
        }
    }
}
