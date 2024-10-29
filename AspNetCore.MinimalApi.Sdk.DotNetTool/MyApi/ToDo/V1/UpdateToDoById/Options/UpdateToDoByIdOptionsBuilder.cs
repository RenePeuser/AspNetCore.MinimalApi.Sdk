using System.CommandLine;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.UpdateToDoById
{
    internal static class AddUpdateToDoByIdOptionsBuilderExtension
    {
        internal static void AddUpdateToDoByIdOptionsBuilder(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<UpdateToDoByIdOptionsBuilder>();
        }
    }

    internal sealed class UpdateToDoByIdOptionsBuilder
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