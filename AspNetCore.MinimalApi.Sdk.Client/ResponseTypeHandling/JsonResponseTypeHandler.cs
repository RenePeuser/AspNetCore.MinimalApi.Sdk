using System.Text.Json;
using Extensions.Pack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.Client
{
    public static class AddJsonResponseTypeHandlerExtension
    {
        public static void AddJsonResponseTypeHandler(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<ISpecificResponseTypeHandler, JsonResponseTypeHandler>();
        }
    }

    internal class JsonResponseTypeHandler : ISpecificResponseTypeHandler
    {
        private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

        public bool CanHandle<TResult>(HttpResponseMessage responseMessage)
        {
            return responseMessage.IsSuccessStatusCode &&
                   typeof(TResult) != typeof(byte[]) &&
                   typeof(TResult) != typeof(FileStreamResult);
        }

        public async Task<TResult> HandleAsync<TResult>(HttpResponseMessage responseMessage,
                                                        HttpMethod httpMethod,
                                                        HttpClient httpClient,
                                                        string url)
        {
            // Safety first this method can be called without CanHandle check !
            CanHandle<TResult>(responseMessage);

            var content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonSerializer.Deserialize<TResult>(content, _options);
            if (result.IsNull())
            {
                throw new JsonDeserializeException<TResult>(content);
            }

            return result;
        }
    }
}
