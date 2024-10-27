using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.AddToDo
{
    internal static class AddAddToDoHandlerExtension
    {
        internal static void AddAddToDoHandler(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMyApiHttpClientFactory(configuration);
            services.AddSingletonIfNotExists<AddToDoHandler>();
        }
    }

    internal sealed class AddToDoHandler(MyApiHttpClientFactory myApiHttpClientFactory,
                                         ConsoleService consoleService)

    {
        public async Task HandleAsync(AddToDoParameters parameters)
        {
            // 1. create client
            var httpCallHandler = myApiHttpClientFactory.CreateFrom(parameters.Token);
            // 2. Convert into target type
            var callInfos = parameters.Json.FromJsonStringAs<Dictionary<string, object?>>();
            // 3. Setup all needed variables
            var todo = callInfos["todo"]; // Mandatory
            var result = await httpCallHandler.CallAsync(HttpMethod.Post, $"api/v1.0/todos", todo, CancellationToken.None).ConfigureAwait(false);

            consoleService.WriteSuccess(result);
        }
    }
}
