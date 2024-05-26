using System;
using Microsoft.Extensions.DependencyInjection;
using AspNetCore.MinimalApi.Sdk.Client.Api.ToDo.V1;
using AspNetCore.MinimalApi.Sdk.Client.Api.ToDo.V2;

namespace AspNetCore.MinimalApi.Sdk.Client.Api.ToDo
{
    public static class AddToDoFacadeExtension
    {
        public static void AddToDoFacade(this IServiceCollection services)
        {
			services.AddToDoV1();
			services.AddToDoV2();
        }
    }

    public class ToDoFacade
    {  
        public ToDoFacade(ToDoV1 toDoV1, ToDoV2 toDoV2)
        {
			V1 = toDoV1;
			V2 = toDoV2;
        }

        
        public ToDoV1 V1 { get; init; }
        
        public ToDoV2 V2 { get; init; }
    }
}