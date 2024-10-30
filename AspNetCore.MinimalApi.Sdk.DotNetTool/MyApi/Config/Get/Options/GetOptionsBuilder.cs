using System.CommandLine;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Config.Get
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
            yield return BuildformatOption();
            yield return BuildOutputOption();
        }

        private Option BuildformatOption()
        {
            return new Option(["--format", "-f"], "Provide the format in which type the output should be created. Choose an an available option")
            {
                Required = false,
                Argument = new Argument<FormatType>("format")
                {
                    Description = "Provide the format in which type the output should be created. Choose an an available option"
                }
            };
        }

        private Option BuildOutputOption()
        {
            return new Option(["--output", "-o"], "Writes the output in your provided file. Sample: 'D:/Documents/MyFile.txt'")
            {
                Required = false,
                Argument = new Argument<FileInfo?>("output")
                {
                    Description = "Writes the output in your provided file. Sample: 'D:/Documents/MyFile.txt'"
                }
            };
        }
    }
}
