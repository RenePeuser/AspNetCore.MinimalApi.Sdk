using Microsoft.Extensions.DependencyInjection;
using AspNetCore.MinimalApi.Sdk.Client.Api.ToDo;

namespace AspNetCore.MinimalApi.Sdk.Client
{
    public static class AddAspNetCoreMinimalApiSdkClientExtension
    {
        public static void AddAspNetCoreMinimalApiSdkClient(this IServiceCollection services)
        {
			services.AddToDoFacade();
        }
    }

    public class AspNetCoreMinimalApiSdkClient
    {  
        public AspNetCoreMinimalApiSdkClient(ToDoFacade toDoFacade)
        {
			ToDo = toDoFacade;
        }

        
        public ToDoFacade ToDo { get; init; }
    }
}