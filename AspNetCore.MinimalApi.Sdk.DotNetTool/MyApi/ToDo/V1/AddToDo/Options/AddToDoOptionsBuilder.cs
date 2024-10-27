using System.CommandLine;
using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.AddToDo
{
    internal static class AddAddToDoOptionsBuilderExtension
    {
        internal static void AddAddToDoOptionsBuilder(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<AddToDoOptionsBuilder>();
        }
    }

    internal sealed class AddToDoOptionsBuilder
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