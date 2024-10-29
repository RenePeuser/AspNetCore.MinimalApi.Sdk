using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    public static class AddOutputWriterExtension
    {
        public static void AddOutputWriter(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<OutputWriter>();
        }
    }

    internal sealed class OutputWriter(ConsoleService consoleService)
    {
        internal async Task WriteAsync(string value, FileInfo? fileInfo)
        {
            if (fileInfo.IsNull())
            {
                consoleService.WriteSuccess(value);
                return;
            }

            if (fileInfo.Directory.IsNull() ||
                fileInfo.Directory.Name.IsNullOrWhiteSpace())
            {
                throw new ProblemDetailsException("Directory must not be NULL, Empty or Whitspace please check you passed value for your output.",
                                                  "Pelease check you passed value for your output.",
                                                  ("FileInfo", fileInfo.FullName),
                                                  ("Output", value));
            }

            if (fileInfo.Directory.NotExists())
            {
                fileInfo.Directory.Create();
            }

            await File.WriteAllTextAsync(fileInfo.FullName, value).ConfigureAwait(false);

            consoleService.WriteSuccess("Output successfully written into file:");
            consoleService.WriteSuccess(fileInfo.FullName);
        }
    }
}
