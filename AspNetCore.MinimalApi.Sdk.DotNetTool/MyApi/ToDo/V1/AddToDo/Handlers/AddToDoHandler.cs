using Extensions.Pack;

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

    internal sealed class AddToDoHandler(MyApiHttpClientFactory myApiHttpClientFactory)
    {
        internal async Task HandleAsync(AddToDoParameters parameters)
        {
            // 1. create client
            var httpCallHandler = myApiHttpClientFactory.CreateFrom(parameters.Token);

            // 2. Convert into target type
            var callInfos = parameters.Json.FromJsonStringAs<Dictionary<string, object?>>();

            // 3. Setup all needed variables
            var todo = callInfos["todo"]; // Mandatory

            // 4. Get the response as string -> only OK responses are provided here, error will be handled as exception
            var stringResponse = await httpCallHandler.CallAsync(HttpMethod.Post, $"api/v1.0/todos", todo, CancellationToken.None).ConfigureAwait(false);
        }
    }
}
