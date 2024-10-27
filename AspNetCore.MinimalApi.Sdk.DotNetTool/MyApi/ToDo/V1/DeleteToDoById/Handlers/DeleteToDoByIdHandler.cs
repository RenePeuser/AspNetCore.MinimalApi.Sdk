using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.DeleteToDoById
{
    internal static class AddDeleteToDoByIdHandlerExtension
    {
        internal static void AddDeleteToDoByIdHandler(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMyApiHttpClientFactory(configuration);
            services.AddSingletonIfNotExists<DeleteToDoByIdHandler>();
        }
    }

    internal sealed class DeleteToDoByIdHandler(MyApiHttpClientFactory myApiHttpClientFactory)
    {
        public Task HandleAsync(DeleteToDoByIdParameters parameters)
        {
            // 1. create client
            var httpCallHandler = myApiHttpClientFactory.CreateFrom(parameters.Token);
            // 2. Convert into target type
            var callInfos = parameters.Json.FromJsonStringAs<Dictionary<string, object?>>();
            // 3. Setup all needed variables
            var id = callInfos["id"]; // Mandatory
            return httpCallHandler.CallAsync(HttpMethod.Delete, $"api/v1.0/todos/{id}", null, CancellationToken.None);
        }
    }
}