using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Config.Set
{
    internal static class AddSetHandlerExtension
    {
        internal static void AddSetHandler(this IServiceCollection services, IConfiguration _)
        {
            services.AddSingletonIfNotExists<SetHandler>();
        }
    }

    internal sealed class SetHandler(OutputService outputService)
    {
        internal async Task HandleAsync(SetParameters setParameters)
        {
            // 1. Define target file
            var appsettingsOnDisk = new FileInfo(Path.Combine(Environment.CurrentDirectory, "appsettings.json"));

            // 2. Write output
            await outputService.WriteAsync(setParameters.Json, appsettingsOnDisk, FormatType.JsonIndented).ConfigureAwait(false);
        }
    }
}
