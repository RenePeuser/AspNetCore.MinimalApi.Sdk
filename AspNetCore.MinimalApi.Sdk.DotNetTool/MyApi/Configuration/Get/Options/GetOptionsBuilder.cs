using System.CommandLine;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Configuration.Get.Options
{
    internal static class AddGetOptionsBuilderExtension
    {
        internal static void AddGetOptionsBuilder(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<GetOptionsBuilder>();
        }
    }

    internal sealed class GetOptionsBuilder
    {
        internal IEnumerable<Option> Build()
        {
            yield return BuildFormatOption();
            yield return BuildOutputOption();
        }

        private Option BuildFormatOption()
        {
            return new Option(["--format", "-f"], "Provides in which type the output should be created")
            {
                Required = false,
                Argument = new Argument<FormatType>
                {
                    Name = "format",
                    Description = "You can pass an available format, as long the endpoint provides to transform the output into the requested format."
                }
            };
        }

        private Option BuildOutputOption()
        {
            return new Option(["--output", "-o"], "Provides in which type the output should be created")
            {
                Required = false,
                Argument = new Argument<FileInfo>
                {
                    Name = "output",
                    Description = @"Writes the output in your provided file. Sample: 'D:\Documents\MyFile.txt'"
                }
            };
        }
    }
}
