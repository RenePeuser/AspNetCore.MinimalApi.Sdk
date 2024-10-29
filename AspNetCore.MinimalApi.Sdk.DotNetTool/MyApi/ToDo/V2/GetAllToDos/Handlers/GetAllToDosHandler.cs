using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V2.GetAllToDos
{
    internal static class AddGetAllToDosHandlerExtension
    {
        internal static void AddGetAllToDosHandler(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMyApiHttpClientFactory(configuration);
            services.AddSingletonIfNotExists<GetAllToDosHandler>();
        }
    }

    internal sealed class GetAllToDosHandler(ConsoleService consoleService, MyApiHttpClientFactory myApiHttpClientFactory)
    {
        public async Task HandleAsync(GetAllToDosParameters parameters)
        {
            // 1. create client
            var httpCallHandler = myApiHttpClientFactory.CreateFrom(parameters.Token);
            // Get the response as string -> only OK responses are provided here, error will be handled as exception
            var stringResponse = await httpCallHandler.CallAsync(HttpMethod.Get, $"api/v2.0/todos", null, CancellationToken.None).ConfigureAwait(false);
            consoleService.WriteSuccess(stringResponse);
        }
    }
}