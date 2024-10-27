using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.GetAllToDos
{
    internal static class AddGetAllToDosHandlerExtension
    {
        internal static void AddGetAllToDosHandler(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMyApiHttpClientFactory(configuration);
            services.AddConsoleService();

            services.AddSingletonIfNotExists<GetAllToDosHandler>();
        }
    }

    internal sealed class GetAllToDosHandler(MyApiHttpClientFactory myApiHttpClientFactory,
                                             ConsoleService consoleService)
    {
        public async Task HandleAsync(GetAllToDosParameters parameters)
        {
            // 1. create client
            var httpCallHandler = myApiHttpClientFactory.CreateFrom(parameters.Token);
            var result = await httpCallHandler.CallAsync(HttpMethod.Get, $"api/v1.0/todos", null, CancellationToken.None).ConfigureAwait(false);

            consoleService.WriteSuccess(result);
        }
    }
}
