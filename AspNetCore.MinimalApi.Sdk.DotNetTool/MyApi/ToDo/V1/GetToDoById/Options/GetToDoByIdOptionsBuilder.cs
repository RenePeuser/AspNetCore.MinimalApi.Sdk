using System.CommandLine;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.GetToDoById
{
    internal static class AddGetToDoByIdOptionsBuilderExtension
    {
        internal static void AddGetToDoByIdOptionsBuilder(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<GetToDoByIdOptionsBuilder>();
        }
    }

    internal sealed class GetToDoByIdOptionsBuilder
    {
        internal IEnumerable<Option> Build()
        {
            yield return BuildformatOption();
            yield return BuildOutputOption();
            yield return BuildtokenOption();
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

        private Option BuildtokenOption()
        {
            return new Option(["--token", "-t"], "Bearer token for authentication")
            {
                Required = false,
                Argument = new Argument<string>("token")
                {
                    Description = "Bearer token for authentication"
                }
            };
        }
    }
}