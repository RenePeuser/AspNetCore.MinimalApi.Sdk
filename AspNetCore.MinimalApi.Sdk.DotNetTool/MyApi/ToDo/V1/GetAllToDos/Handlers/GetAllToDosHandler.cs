using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.GetAllToDos
{
    internal static class AddGetAllToDosHandlerExtension
    {
        internal static void AddGetAllToDosHandler(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMyApiHttpClientFactory(configuration);
            services.AddSingletonIfNotExists<GetAllToDosHandler>();
        }
    }

    internal sealed class GetAllToDosHandler(MyApiHttpClientFactory myApiHttpClientFactory)
    {
        public Task HandleAsync(GetAllToDosParameters parameters)
        {
            // 1. create client
            var httpCallHandler = myApiHttpClientFactory.CreateFrom(parameters.Token);
            return httpCallHandler.CallAsync(HttpMethod.Get, $"api/v1.0/todos", null, CancellationToken.None);
        }
    }
}