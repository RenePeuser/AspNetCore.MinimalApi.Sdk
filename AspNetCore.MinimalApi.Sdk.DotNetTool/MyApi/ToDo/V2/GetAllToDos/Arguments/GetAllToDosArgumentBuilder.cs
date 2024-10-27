using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V2.GetAllToDos
{
    internal static class AddGetAllToDosArgumentBuilderExtension
    {
        internal static void AddGetAllToDosArgumentBuilder(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<GetAllToDosArgumentBuilder>();
        }
    }

    internal sealed class GetAllToDosArgumentBuilder
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