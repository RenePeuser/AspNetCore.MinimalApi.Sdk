using System.Net.Http;
using System.Threading.Tasks;
using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.Client
{
    public static class AddNotOkResponseTypeHandlerExtension
    {
        public static void AddNotOkResponseTypeHandler(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<ISpecificResponseTypeHandler, NotOkResponseTypeHandler>();
        }
    }

    internal class NotOkResponseTypeHandler : ISpecificResponseTypeHandler
    {
        public bool CanHandle<TResult>(HttpResponseMessage responseMessage)
        {
            return responseMessage.IsSuccessStatusCode.IsFalse();
        }

        public async Task<TResult> HandleAsync<TResult>(HttpResponseMessage responseMessage, HttpMethod httpMethod, HttpClient httpClient, string url)
        {
            // Safety first this method can be called without CanHandle check !
            CanHandle<TResult>(responseMessage);

            var content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            var absoluteUrl = $"{httpClient.BaseAddress}{url}";
            throw new ClientProblemDetailsException("Client call to endpoint was not successfull",
                                                    $"The http call: {httpMethod.Method} {url} was not succesfull",
                                                    ("HttpMethod", httpMethod.Method),
                                                    ("Url", absoluteUrl),
                                                    ("Error", content));
        }
    }
}
