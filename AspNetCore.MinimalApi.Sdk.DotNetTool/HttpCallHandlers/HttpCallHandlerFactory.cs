using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    internal static class AddHttpCallHandlerFactoryExtension
    {
        internal static void AddHttpCallHandlerFactory(this IServiceCollection services)
        {
            services.AddResponseTypeHandleStrategy();
            services.AddHttpRequestMessageBuilder();
            services.AddSingletonIfNotExists<HttpCallHandlerFactory>();
        }
    }

    internal sealed class HttpCallHandlerFactory
    {
        private readonly HttpRequestMessageBuilder _httpRequestMessageBuilder;
        private readonly ResponseTypeHandleStrategy _responseTypeHandleStrategy;
        public HttpCallHandlerFactory(ResponseTypeHandleStrategy responseTypeHandleStrategy, HttpRequestMessageBuilder httpRequestMessageBuilder)
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