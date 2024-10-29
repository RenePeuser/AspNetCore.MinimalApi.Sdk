using System.Net.Http.Headers;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    internal static class AddMyApiHttpClientFactoryExtension
    {
        internal static void AddMyApiHttpClientFactory(this IServiceCollection services, IConfiguration configuration)
        {
            if (services.IsAlreadyRegistered<MyApiHttpClientFactory>())
            {
                return;
            }

            services.AddHttpClient();
            services.AddMyApiHttpClientSettings(configuration);
            services.AddHttpCallHandlerFactory();
            services.AddSingletonIfNotExists<MyApiHttpClientFactory>();
        }
    }

    internal sealed class MyApiHttpClientFactory(IHttpClientFactory httpClientFactory, MyApiHttpClientSettings aspNetCoreMinimalApiSdkClientSettings, HttpCallHandlerFactory httpCallHandlerFactory)
    {
        private readonly string[] _allowedHeadersToCopy =
        {
            "User-Agent",
            "Authorization",
            "x-",
            "Referer"
        };

        internal HttpCallHandler CreateFrom(HttpRequest httpRequest)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpRequest.Headers.ForEach(headerEntry =>
            {
                // Important not content and host header copy
                if (_allowedHeadersToCopy.Any(allowedHeader => headerEntry.Key.Contains(allowedHeader, StringComparison.OrdinalIgnoreCase)))
                {
                    httpClient.DefaultRequestHeaders.Add(headerEntry.Key, headerEntry.Value.FirstOrDefault() ?? string.Empty);
                }
            });
            httpClient.BaseAddress = new Uri(aspNetCoreMinimalApiSdkClientSettings.BaseAddress);
            var httpClientHandler = httpCallHandlerFactory.CreateFrom(httpClient);
            return httpClientHandler;
        }

        internal HttpCallHandler CreateFrom(string schema, string token)
        {
            return CreateFrom($"{schema} {token}");
        }

        internal HttpCallHandler CreateFrom(string token)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(aspNetCoreMinimalApiSdkClientSettings.BaseAddress);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "DotNetToolName");
            var httpClientHandler = httpCallHandlerFactory.CreateFrom(httpClient);
            return httpClientHandler;
        }
    }

    internal static class AddMyApiHttpClientSettingsExtension
    {
        internal static void AddMyApiHttpClientSettings(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.TryGetSettings<MyApiHttpClientSettings>(out var clientSettings).IsFalse())
            {
                clientSettings = new MyApiHttpClientSettings();
            }

            services.AddSingletonIfNotExists(clientSettings);
        }
    }

    internal sealed record MyApiHttpClientSettings
    {
        public string BaseAddress { get; init; } = "http://staging/api/MyApi/";
    }
}
