using Extensions.Pack;

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

    internal sealed class GetAllToDosHandler(OutputService outputService, MyApiHttpClientFactory myApiHttpClientFactory)
    {
        public async Task HandleAsync(GetAllToDosParameters parameters)
        {
            // 1. create client
            var httpCallHandler = myApiHttpClientFactory.CreateFrom(parameters.Token);
            // Get the response as string -> only OK responses are provided here, error will be handled as exception
            var stringResponse = await httpCallHandler.CallAsync(HttpMethod.Get, $"api/v1.0/todos", null, CancellationToken.None).ConfigureAwait(false);
            // 3. Write the formatted string to the output
            await outputService.WriteAsync(stringResponse, parameters.Output, parameters.Format).ConfigureAwait(false);
        }
    }
}