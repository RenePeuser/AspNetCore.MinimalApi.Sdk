using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.DeleteToDoById
{
    internal static class AddDeleteToDoByIdArgumentBuilderExtension
    {
        internal static void AddDeleteToDoByIdArgumentBuilder(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<DeleteToDoByIdArgumentBuilder>();
        }
    }

    internal sealed class DeleteToDoByIdArgumentBuilder
    {
        public System.CommandLine.Argument Build()
        {
            var argument = new System.CommandLine.Argument<string>()
            {
                Name = "json",
                Description = "Call info as json which contains url- and query parameters as well the payload if needed"
            };
            return argument;
        }
    }
}