using System.Net.Http.Headers;
using AspNetCore.MinimalApi.Sdk.Client.Api.ToDo;
using AspNetCore.MinimalApi.Sdk.Client.Api.ToDo.V1;
using AspNetCore.MinimalApi.Sdk.Client.Api.ToDo.V2;
using Extensions.Pack;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.Client
{
    public static class AddAspNetCoreMinimalApiSdkClientFactoryExtension
    {
        public static void AddAspNetCoreMinimalApiSdkClientFactory(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddAspNetCoreMinimalApiSdkClientSettings(configuration);
            services.AddResponseTypeHandleStrategy();

            services.AddSingletonIfNotExists<IAspNetCoreMinimalApiSdkClientFactory, AspNetCoreMinimalApiSdkClientFactory>();
        }
    }

    public interface IAspNetCoreMinimalApiSdkClientFactory
    {
        AspNetCoreMinimalApiSdkClient CreateFrom(HttpClient sourceClient);
        AspNetCoreMinimalApiSdkClient CreateFrom(HttpRequest httpRequest);
    }

    internal class AspNetCoreMinimalApiSdkClientFactory : IAspNetCoreMinimalApiSdkClientFactory
    {
        private readonly string[] _allowedHeadersToCopy = { "User-Agent", "Authorization", "x-", "Referer" };
        private readonly AspNetCoreMinimalApiSdkClientSettings _AspNetCoreMinimalApiSdkClientSettings;
        private readonly HttpCallHandlerFactory _httpCallHandlerFactory;
        private readonly IHttpClientFactory _httpClientFactory;

        public AspNetCoreMinimalApiSdkClientFactory(IHttpClientFactory httpClientFactory,
                                                    AspNetCoreMinimalApiSdkClientSettings AspNetCoreMinimalApiSdkClientSettings,
                                                    HttpCallHandlerFactory httpCallHandlerFactory)
        {
            _httpClientFactory = httpClientFactory;
            _AspNetCoreMinimalApiSdkClientSettings = AspNetCoreMinimalApiSdkClientSettings;
            _httpCallHandlerFactory = httpCallHandlerFactory;
        }

        public AspNetCoreMinimalApiSdkClient CreateFrom(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri(_AspNetCoreMinimalApiSdkClientSettings.BaseAddress);
            var httpClientHandler = _httpCallHandlerFactory.CreateFrom(httpClient);

            var client = BuildClient(httpClientHandler);

            return client;
        }

        public AspNetCoreMinimalApiSdkClient CreateFrom(HttpRequest httpRequest)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpRequest.Headers.ForEach(headerEntry =>
            {
                // Important not content and host header copy
                if (_allowedHeadersToCopy.Any(allowedHeader => headerEntry.Key.Contains(allowedHeader, StringComparison.OrdinalIgnoreCase)))
                {
                    httpClient.DefaultRequestHeaders.Add(headerEntry.Key, headerEntry.Value.FirstOrDefault() ?? string.Empty);
                }
            });

            httpClient.BaseAddress = new Uri(_AspNetCoreMinimalApiSdkClientSettings.BaseAddress);

            var httpClientHandler = _httpCallHandlerFactory.CreateFrom(httpClient);

            var client = BuildClient(httpClientHandler);

            return client;
        }

        public AspNetCoreMinimalApiSdkClient CreateFrom(string token)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_AspNetCoreMinimalApiSdkClientSettings.BaseAddress);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var httpClientHandler = _httpCallHandlerFactory.CreateFrom(httpClient);

            var client = BuildClient(httpClientHandler);

            return client;
        }

        private AspNetCoreMinimalApiSdkClient BuildClient(HttpCallHandler httpClientHandler)
        {
            return new AspNetCoreMinimalApiSdkClient(new ToDoFacade(new ToDoV1(httpClientHandler), new ToDoV2(httpClientHandler)));
        }
    }

    public static class AddAspNetCoreMinimalApiSdkClientSettingsExtension
    {
        public static void AddAspNetCoreMinimalApiSdkClientSettings(this IServiceCollection services, IConfiguration configuration)
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
