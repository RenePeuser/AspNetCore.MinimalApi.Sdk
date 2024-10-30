using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Config.Set
{
    internal static class AddSetArgumentBuilderExtension
    {
        internal static void AddSetArgumentBuilder(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<SetArgumentBuilder>();
        }
    }

    internal sealed class SetArgumentBuilder
    {
        internal System.CommandLine.Argument Build()
        {
            var argument = new System.CommandLine.Argument<string>()
            {
                Name = "json",
                Description = "Provide the new config json which is known as appsettings.json"
            };
            return argument;
        }
    }
}