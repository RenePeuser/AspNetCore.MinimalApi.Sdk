using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.UpdateToDoById
{
    internal static class AddUpdateToDoByIdHandlerExtension
    {
        internal static void AddUpdateToDoByIdHandler(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMyApiHttpClientFactory(configuration);
            services.AddSingletonIfNotExists<UpdateToDoByIdHandler>();
        }
    }

    internal sealed class UpdateToDoByIdHandler(ConsoleService consoleService, MyApiHttpClientFactory myApiHttpClientFactory)
    {
        public async Task HandleAsync(UpdateToDoByIdParameters parameters)
        {
            // 1. create client
            var httpCallHandler = myApiHttpClientFactory.CreateFrom(parameters.Token);
            // 2. Convert into target type
            var callInfos = parameters.Json.FromJsonStringAs<Dictionary<string, object?>>();
            // 3. Setup all needed variables
            var id = callInfos["id"]; // Mandatory
            var todo = callInfos["todo"]; // Mandatory
            // Get the response as string -> only OK responses are provided here, error will be handled as exception
            var stringResponse = await httpCallHandler.CallAsync(HttpMethod.Put, $"api/v1.0/todos/{id}", todo, CancellationToken.None).ConfigureAwait(false);
            consoleService.WriteSuccess(stringResponse);
        }
    }
}