using Extensions.Pack;
using Microsoft.Extensions.Configuration.Json;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    internal sealed class AppBuilder
    {
        internal App Build()
        {
            var startup = new Startup();
            var services = new ServiceCollection();
            var configurationBuilder = new ConfigurationBuilder();
            var appsettingsAsStream = typeof(AppBuilder).Assembly.GetEmbeddedFileAsStream("appsettings.json");
            var jsonStreamConfigurationSource = new JsonStreamConfigurationSource
            {
                Stream = appsettingsAsStream
            };
            configurationBuilder.Add(jsonStreamConfigurationSource);
            configurationBuilder.AddEnvironmentVariables();
            configurationBuilder.AddUserSecrets(typeof(AppBuilder).Assembly);
            var configuration = configurationBuilder.Build();
            startup.ConfigureServices(services, configuration);
            var buildServiceProvider = services.BuildServiceProvider();
            return new App(buildServiceProvider);
        }
    }
}
