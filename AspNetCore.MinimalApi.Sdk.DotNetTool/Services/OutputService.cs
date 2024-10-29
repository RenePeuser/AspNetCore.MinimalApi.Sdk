using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    public static class AddOutputServiceExtension
    {
        public static void AddOutputService(this IServiceCollection services)
        {
            services.AddOutputFormatter();
            services.AddOutputWriter();

            services.AddSingletonIfNotExists<OutputService>();
        }
    }

    internal sealed class OutputService(OutputFormatter outputFormatter,
                                        OutputWriter outputWriter)
    {
        internal async Task WriteAsync(string value,
                                       FileInfo? fileInfo,
                                       FormatType formatType)
        {
            // 1. Format the string into expected format
            var formattedString = outputFormatter.Format(value, formatType);

            // 3. Write the formatted string to the output
            await outputWriter.WriteAsync(formattedString, fileInfo).ConfigureAwait(false);
        }
    }
}
