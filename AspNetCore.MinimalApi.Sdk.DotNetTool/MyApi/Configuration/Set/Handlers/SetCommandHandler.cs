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

    internal sealed class SetCommandHandler(ConsoleService consoleService)
    {
        public async Task HandleAsync(GetParameters setParameters)
        {
            // 1. Check if an appsettings.json exists on file
            var appsettingsOnDisk = new FileInfo(Path.Combine(Environment.CurrentDirectory, "appsettings.json"));
            await File.WriteAllTextAsync(appsettingsOnDisk.FullName, setParameters.Json).ConfigureAwait(false);

            consoleService.WriteSuccess($"New appsettings successful written into: {appsettingsOnDisk.FullName}.");
        }
    }
}
