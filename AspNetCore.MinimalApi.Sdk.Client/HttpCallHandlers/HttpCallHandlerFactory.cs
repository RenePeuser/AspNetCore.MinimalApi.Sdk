using System.Net.Http;
using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.Client
{
    public static class AddHttpCallHandlerFactoryExtension
    {
        public static void AddHttpCallHandlerFactory(this IServiceCollection services)
        {
            services.AddResponseTypeHandleStrategy();
            services.AddHttpRequestMessageBuilder();

            services.AddSingletonIfNotExists<HttpCallHandlerFactory>();
        }
    }

    internal class HttpCallHandlerFactory
    {
        private readonly ResponseTypeHandleStrategy _responseTypeHandleStrategy;
        private readonly HttpRequestMessageBuilder _httpRequestMessageBuilder;

        public HttpCallHandlerFactory(ResponseTypeHandleStrategy responseTypeHandleStrategy,
                                      HttpRequestMessageBuilder httpRequestMessageBuilder)
        {
            _responseTypeHandleStrategy = responseTypeHandleStrategy;
            _httpRequestMessageBuilder = httpRequestMessageBuilder;
        }

        internal HttpCallHandler CreateFrom(HttpClient client)
        {
            return new HttpCallHandler(client, _responseTypeHandleStrategy, _httpRequestMessageBuilder);
        }
    }
}
