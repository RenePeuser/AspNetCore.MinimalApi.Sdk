using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    internal static class AddMyApiArgumentFixerExtension
    {
        internal static void AddMyApiArgumentFixer(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<MyApiArgumentFixer>();
        }
    }

    internal sealed class MyApiArgumentFixer
    {
        public string[] Fix(string[] args)
        {
            var defaultArgs = new[]
            {
                "myapi"
            };
            var newArgs = defaultArgs.Concat(args).Distinct().ToList();
            return newArgs.ToArray();
        }
    }
}