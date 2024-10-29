using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.AddToDo
{
    internal static class AddAddToDoArgumentBuilderExtension
    {
        internal static void AddAddToDoArgumentBuilder(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<AddToDoArgumentBuilder>();
        }
    }

    internal sealed class AddToDoArgumentBuilder
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
