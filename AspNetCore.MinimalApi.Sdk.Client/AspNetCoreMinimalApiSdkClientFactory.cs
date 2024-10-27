using System.Net.Http.Headers;
using Extensions.Pack;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.Client
{
    public static class AddAspNetCoreMinimalApiSdkClientFactoryExtension
    {
        public static void AddAspNetCoreMinimalApiSdkClientFactory(this IServiceCollection services,
                                                                   IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddAspNetCoreMinimalApiSdkClientSettings(configuration);
            services.AddResponseTypeHandleStrategy();

            services.AddSingletonIfNotExists<AspNetCoreMinimalApiSdkClientFactory>();
        }
    }

    internal class AspNetCoreMinimalApiSdkClientFactory(IHttpClientFactory httpClientFactory,
                                                        AspNetCoreMinimalApiSdkClientSettings aspNetCoreMinimalApiSdkClientSettings,
                                                        HttpCallHandlerFactory httpCallHandlerFactory)
    {
        private readonly string[] _allowedHeadersToCopy = 
        { 
            "User-Agent", 
            "Authorization", 
            "x-", 
            "Referer" 
        };

        public HttpCallHandler CreateFrom(HttpRequest httpRequest)
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

        public HttpCallHandler CreateFrom(string schema, string token)
        {
            return CreateFrom($"{schema} {token}");
        }
        
        public HttpCallHandler CreateFrom(string token)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(aspNetCoreMinimalApiSdkClientSettings.BaseAddress);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "DotNetToolName");
            var httpClientHandler = httpCallHandlerFactory.CreateFrom(httpClient);

            return httpClientHandler;
        }
    }

    public static class AddAspNetCoreMinimalApiSdkClientSettingsExtension
    {
        public static void AddAspNetCoreMinimalApiSdkClientSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            if (configuration.TryGetSettings<AspNetCoreMinimalApiSdkClientSettings>(out var clientSettings).IsFalse())
            {
                clientSettings = new AspNetCoreMinimalApiSdkClientSettings();
            }

            services.AddSingletonIfNotExists(clientSettings);
        }
    }

    public record AspNetCoreMinimalApiSdkClientSettings
    {
        public string BaseAddress { get; init; } = "http://localhost:6001/";
    }
}
