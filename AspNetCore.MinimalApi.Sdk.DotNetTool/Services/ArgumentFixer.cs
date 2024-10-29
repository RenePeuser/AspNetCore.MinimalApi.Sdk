using Extensions.Pack;

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
        internal string[] Fix(string[] args)
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
