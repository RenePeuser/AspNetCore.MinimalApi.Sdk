using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.Client
{
    public static class AddByteArrayResponseTypeHandlerExtension
    {
        public static void AddByteArrayResponseTypeHandler(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<ISpecificResponseTypeHandler, ByteArrayResponseTypeHandler>();
        }
    }

    internal class ByteArrayResponseTypeHandler : ISpecificResponseTypeHandler
    {
        public bool CanHandle<TResult>(HttpResponseMessage responseMessage)
        {
            return responseMessage.IsSuccessStatusCode &&
                   typeof(TResult) == typeof(byte[]);
        }

        public async Task<TResult> HandleAsync<TResult>(HttpResponseMessage responseMessage, HttpMethod httpMethod, HttpClient httpClient, string url)
        {
            // Safety first this method can be called without CanHandle check !
            CanHandle<TResult>(responseMessage);

            var result = await responseMessage.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            return (TResult)result.Cast<object>();
        }
    }
}
