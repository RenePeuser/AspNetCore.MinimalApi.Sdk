using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.Client
{
    public static class AddHttpRequestMessageBuilderExtension
    {
        public static void AddHttpRequestMessageBuilder(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<HttpRequestMessageBuilder>();
        }
    }

    internal class HttpRequestMessageBuilder
    {
        internal HttpRequestMessage BuildFrom(HttpMethod method, string uri, object? payload, string payloadParameterName)
        {
            var content = payload.IsNotNull() ? GetContent() : null;
            return new HttpRequestMessage(method, uri)
            {
                Version = HttpVersion.Version11,
                VersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
                Content = content
            };

            HttpContent GetContent()
            {
                switch (payload)
                {
                    case string stringContent:
                        return new StringContent(stringContent);
                    case InMemoryFileAsStream inMemoryFileAsStream:
                        return inMemoryFileAsStream.ToMultipartFormDataContent(payloadParameterName);
                    case byte[] byteArray:
                        return new ByteArrayContent(byteArray);
                    case Stream streamContent:
                        return new StreamContent(streamContent);
                    default:
                        // Default any class or record converted to json string
                        return new StringContent(System.Text.Json.JsonSerializer.Serialize(payload), Encoding.UTF8, MediaTypeNames.Application.Json);
                }
            }
        }
    }
}
