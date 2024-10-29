using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.GetToDoById
{
    internal static class AddGetToDoByIdArgumentBuilderExtension
    {
        internal static void AddGetToDoByIdArgumentBuilder(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<GetToDoByIdArgumentBuilder>();
        }
    }

    internal sealed class GetToDoByIdArgumentBuilder
    {
        internal System.CommandLine.Argument Build()
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
