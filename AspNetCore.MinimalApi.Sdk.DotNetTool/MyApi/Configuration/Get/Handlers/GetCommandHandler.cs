using System.Text.Json;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Configuration.Get
{
    internal static class AddGetCommandHandlerExtension
    {
        internal static void AddGetCommandHandler(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<GetCommandHandler>();
        }
    }

    internal sealed class GetCommandHandler(ConsoleService consoleService)
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions() { WriteIndented = false };

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

            // 3. If FormatAsString is true, re-serialize to a single-line JSON string
            if (getParameters.FormatAsString)
            {
                var jsonObject = JsonSerializer.Deserialize<object>(appsettings);
                appsettings = JsonSerializer.Serialize(jsonObject, _jsonSerializerOptions);

                // Escape double quotes
                appsettings = appsettings.Replace("\"", "\\\"");

                // Wrap it in double quotes to produce the desired output
                appsettings = $"\"{appsettings}\"";

            }

            consoleService.WriteSuccess(appsettings);
        }
    }
}
