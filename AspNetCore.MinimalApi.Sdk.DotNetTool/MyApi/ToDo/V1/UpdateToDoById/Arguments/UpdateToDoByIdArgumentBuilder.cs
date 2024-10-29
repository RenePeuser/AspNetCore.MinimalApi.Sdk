using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.UpdateToDoById
{
    internal static class AddUpdateToDoByIdArgumentBuilderExtension
    {
        internal static void AddUpdateToDoByIdArgumentBuilder(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<UpdateToDoByIdArgumentBuilder>();
        }
    }

    internal sealed class UpdateToDoByIdArgumentBuilder
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
