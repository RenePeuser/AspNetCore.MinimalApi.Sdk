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

    public sealed class HttpCallHandlerFactory(ResponseTypeHandleStrategy responseTypeHandleStrategy,
                                               HttpRequestMessageBuilder httpRequestMessageBuilder)
    {
        internal HttpCallHandler CreateFrom(HttpClient client)
        {
            return new HttpCallHandler(client, responseTypeHandleStrategy, httpRequestMessageBuilder);
        }
    }
}
