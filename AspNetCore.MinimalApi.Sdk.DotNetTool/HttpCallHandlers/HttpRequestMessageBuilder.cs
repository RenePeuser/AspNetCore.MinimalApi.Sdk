using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    public static class AddHttpRequestMessageBuilderExtension
    {
        public static void AddHttpRequestMessageBuilder(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<HttpRequestMessageBuilder>();
        }
    }

    public sealed class HttpRequestMessageBuilder
    {
        internal HttpRequestMessage BuildFrom(HttpMethod method, string uri, object? payload, string payloadParameterName)
        {
            var content = payload.IsNotNull() ? GetContent(payload, payloadParameterName) : null;
            return new HttpRequestMessage(method, uri)
            {
                Version = HttpVersion.Version11,
                VersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
                Content = content
            };
            static HttpContent GetContent(object? payload, string payloadParameterName)
            {
                return payload switch
                {
                    string stringContent => new StringContent(stringContent),
                    InMemoryFileAsStream inMemoryFileAsStream => inMemoryFileAsStream.ToMultipartFormDataContent(payloadParameterName),
                    byte[] byteArray => new ByteArrayContent(byteArray),
                    Stream streamContent => new StreamContent(streamContent),
                    _ => new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, MediaTypeNames.Application.Json), // Default any class or record converted to json string
                };
            }
        }
    }
}
