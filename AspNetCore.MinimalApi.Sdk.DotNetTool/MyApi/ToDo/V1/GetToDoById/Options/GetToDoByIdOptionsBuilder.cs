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
            yield return BuildtokenOption();
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
