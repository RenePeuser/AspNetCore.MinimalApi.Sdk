using System.CommandLine;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Configuration.Get.Options
{
    internal static class AddGetOptionsBuilderExtension
    {
        internal static void AddGetOptionsBuilder(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<GetOptionsBuilder>();
        }
    }

    internal sealed class GetOptionsBuilder
    {
        public IEnumerable<Option> Build()
        {
            yield return BuildFormatAsStringOption();
        }

        private Option BuildFormatAsStringOption()
        {
            return new Option(["--formatAsString", "-fas"], "Provides the output as flatten string")
            {
                Required = false,
            };
        }
    }
}
