using DotNetTool.Service;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.Test
{
    [TestClass]
    public class GlobalSetup
    {
        protected static IServiceProvider Services { get; private set; } = null!;

        protected static MyAppStarter Cli { get; private set; } = null!;

        [AssemblyInitialize]
        public static void InitAsync(TestContext testContext)
        {
            var dotnetTool = DotNetToolFactory.Create();

            //var configurationBuilder = new ConfigurationBuilder()
            //                           .AddJsonFile("appsettings.json", optional: true)
            //                           .AddJsonFile("appsettings.test.json", optional: true)
            //                           .AddEnvironmentVariables();

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(testContext);
            serviceCollection.AddSingleton<IDotNetTool>(dotnetTool);
            serviceCollection.AddMyAppStarter();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            Services = serviceProvider;
            Cli = serviceProvider.GetRequiredService<MyAppStarter>();
        }
    }
}
