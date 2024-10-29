using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Configuration.Get
{
    internal static class AddGetCommandHandlerExtension
    {
        internal static void AddGetCommandHandler(this IServiceCollection services)
        {
            services.AddOutputService();
            
            services.AddSingletonIfNotExists<GetCommandHandler>();
        }
    }

    internal sealed class GetCommandHandler(OutputService outputService)
    {
        public async Task HandleAsync(GetParameters getParameters)
        {
            // 1. If not provide the embedded version
            var appsettings = EmbeddedFile.GetFileContentFrom("appsettings.json");

            // 2. Check if an appsettings.json exists on file
            var appsettingsOnDisk = new FileInfo(Path.Combine(Environment.CurrentDirectory, "appsettings.json"));
            if (appsettingsOnDisk.Exists)
            {
                appsettings = await File.ReadAllTextAsync(appsettingsOnDisk.FullName).ConfigureAwait(false);
            }

            // 3. Write the formatted string to the output
            await outputService.WriteAsync(appsettings, getParameters.Output, getParameters.Format).ConfigureAwait(false);
        }
    }
}
