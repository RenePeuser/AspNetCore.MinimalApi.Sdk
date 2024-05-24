using Microsoft.AspNetCore.Mvc;
using MinimalApi.Api.ToDos.V1.GetAll;
using MinimalApi.Api.ToDos.V1.GetById;

namespace MinimalApi.Api.ToDos.V1
{
    internal static class Startup
    {
        internal static void AddToDosV1(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddGetAll();
            serviceCollection.AddGetById();
        }

        internal static void UseToDosV1(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            //var v1TodoGroup = endpointRouteBuilder.MapGroup("todos")
            //                                      .WithTags("ToDo")
            //                                      .MapToApiVersion(1);

            var v1TodoGroup = endpointRouteBuilder;
            
            v1TodoGroup.UseGetAll();
            v1TodoGroup.UseGetById();
        }
    }
}
