using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Configuration.Set
{
    internal static class AddSetCommandHandlerExtension
    {
        internal static void AddSetCommandHandler(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<SetCommandHandler>();
        }
    }

    internal sealed class SetCommandHandler(OutputService outputService)
    {
        public async Task HandleAsync(SetParameters setParameters)
        {
            // 1. Define target file
            var appsettingsOnDisk = new FileInfo(Path.Combine(Environment.CurrentDirectory, "appsettings.json"));

            // 2. Write output
            await outputService.WriteAsync(setParameters.Json, appsettingsOnDisk, FormatType.JsonIndented).ConfigureAwait(false);
        }
    }
}
