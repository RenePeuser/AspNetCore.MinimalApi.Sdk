using DotNetTool.Service;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.Test
{
    [TestClass]
    public class GlobalSetup
    {
        /// <summary>
        /// Provides access to the service provider for dependency injection.
        /// </summary>
        protected static IServiceProvider Services { get; private set; } = null!;

        /// <summary>
        /// Provides access to the CLI runner for executing CLI commands.
        /// </summary>
        protected static CliRunner Cli { get; private set; } = null!;

        /// <summary>
        /// Initializes the test environment by setting up the service provider and CLI runner.
        /// This method is called once before any tests are run.
        /// </summary>
        /// <param name="testContext">The context of the test run.</param>
        [AssemblyInitialize]
        public static void InitAsync(TestContext testContext)
        {
            // 1. Create an instance of the DotNetTool.
            var dotnetTool = DotNetToolFactory.Create();

            // 2. Create a new service collection for dependency injection.
            var serviceCollection = new ServiceCollection();

            // 3. Register the test context and DotNetTool as singletons.
            serviceCollection.AddSingleton(testContext);
            serviceCollection.AddSingleton<IDotNetTool>(dotnetTool);

            // 4. Register the application starter extension.
            serviceCollection.AddCliRunner();

            // 5. Build the service provider from the service collection.
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // 6. Assign the service provider and CLI runner to the static properties.
            Services = serviceProvider;
            Cli = serviceProvider.GetRequiredService<CliRunner>();
        }
    }
}
