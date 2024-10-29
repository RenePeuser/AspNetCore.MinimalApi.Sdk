using System.CommandLine;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.DeleteToDoById
{
    internal static class AddDeleteToDoByIdOptionsBuilderExtension
    {
        internal static void AddDeleteToDoByIdOptionsBuilder(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<DeleteToDoByIdOptionsBuilder>();
        }
    }

    internal sealed class DeleteToDoByIdOptionsBuilder
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
