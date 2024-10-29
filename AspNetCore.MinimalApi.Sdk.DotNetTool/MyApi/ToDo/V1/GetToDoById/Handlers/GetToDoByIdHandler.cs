using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.GetToDoById
{
    internal static class AddGetToDoByIdHandlerExtension
    {
        internal static void AddGetToDoByIdHandler(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMyApiHttpClientFactory(configuration);
            services.AddSingletonIfNotExists<GetToDoByIdHandler>();
        }
    }

    internal sealed class GetToDoByIdHandler(ConsoleService consoleService, MyApiHttpClientFactory myApiHttpClientFactory)
    {
        public async Task HandleAsync(GetToDoByIdParameters parameters)
        {
            // 1. create client
            var httpCallHandler = myApiHttpClientFactory.CreateFrom(parameters.Token);
            // 2. Convert into target type
            var callInfos = parameters.Json.FromJsonStringAs<Dictionary<string, object?>>();
            // 3. Setup all needed variables
            var id = callInfos["id"]; // Mandatory
            // Get the response as string -> only OK responses are provided here, error will be handled as exception
            var stringResponse = await httpCallHandler.CallAsync(HttpMethod.Get, $"api/v1.0/todos/{id}", null, CancellationToken.None).ConfigureAwait(false);
            consoleService.WriteSuccess(stringResponse);
        }
    }
}