using Extensions.Pack;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    internal static class AddJsonResponseTypeHandlerExtension
    {
        internal static void AddJsonResponseTypeHandler(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<ISpecificResponseTypeHandler, JsonResponseTypeHandler>();
        }
    }

    internal sealed class JsonResponseTypeHandler : ISpecificResponseTypeHandler
    {
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
            if (CanHandle<TResult>(responseMessage).IsFalse())
            {
                throw new ProblemDetailsException("JsonResponseTypeHandler was called without checking CanHandle.",
                                                  $"The response type handler for type: {typeof(TResult).Name} is not supported",
                                                  ("SupportedTypes", "Serializable content"));
            }

            var content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = content.FromJsonStringAs<TResult>();

            return result;
        }
    }
}
