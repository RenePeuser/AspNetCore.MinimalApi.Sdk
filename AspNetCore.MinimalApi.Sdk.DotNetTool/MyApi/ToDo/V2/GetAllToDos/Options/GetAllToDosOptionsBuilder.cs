using System.CommandLine;
using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V2.GetAllToDos
{
    internal static class AddGetAllToDosOptionsBuilderExtension
    {
        internal static void AddGetAllToDosOptionsBuilder(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<GetAllToDosOptionsBuilder>();
        }
    }

    internal sealed class GetAllToDosOptionsBuilder
    {
        public IEnumerable<Option> Build()
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