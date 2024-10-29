using AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    internal sealed class Startup
    {
        internal void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // 1. Infrastructure
            services.AddMyApiArgumentFixer();
            services.AddErrorHandler();
            // 2. Domains
            services.AddMyApiCommandBuilder(configuration);
        }
    }
}
