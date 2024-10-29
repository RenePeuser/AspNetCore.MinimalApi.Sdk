using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Configuration.Set
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
        public System.CommandLine.Argument Build()
        {
            var argument = new System.CommandLine.Argument<string>()
            {
                Name = "json",
                Description = "The new appsettings json which have to be set and used"
            };
            return argument;
        }
    }
}
