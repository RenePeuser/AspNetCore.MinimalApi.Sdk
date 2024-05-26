using AspNetCore.Simple.MsTest.Sdk;
using Extensions.Pack;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AspNetCore.MinimalApi.Sdk.Client.Test
{

    [TestClass]
    public abstract class ClientTestBase
    {
        private static ApiTestBase<Program>? _customWebApplicationFactory;
        private static HttpClient? _httpClient;

        // In this sample we currently use assembly initialize, to save performance, but you can do it also different.
        [AssemblyInitialize]
        public static Task AssemblyInitializeAsync(TestContext _)
        {
            // Create this with new, is not a fault, the reason is to keep the test class more cleaner.
            _customWebApplicationFactory = new ApiTestBase<Program>("", (services, config) =>
            {
                services.AddAspNetCoreMinimalApiSdkClient();
                services.AddAspNetCoreMinimalApiSdkClientFactory(config);
            });


            var serviceProvider = _customWebApplicationFactory.Services;

            _httpClient = _customWebApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

            // Add your client authentication here !

            var aspNetCoreMinimalApiSdkClientFactory = serviceProvider.GetOrThrowMissingException<IAspNetCoreMinimalApiSdkClientFactory>();
            AspNetCoreMinimalApiSdkClient = aspNetCoreMinimalApiSdkClientFactory.CreateFrom(_httpClient);

            return Task.CompletedTask;
        }

        protected static AspNetCoreMinimalApiSdkClient AspNetCoreMinimalApiSdkClient { get; private set; } = null!;

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            _customWebApplicationFactory?.Dispose();
            _httpClient?.Dispose();
        }
    }
}
