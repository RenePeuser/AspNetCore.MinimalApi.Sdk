using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    internal static class AddNotOkResponseTypeHandlerExtension
    {
        internal static void AddNotOkResponseTypeHandler(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<ISpecificResponseTypeHandler, NotOkResponseTypeHandler>();
        }
    }

    internal sealed class NotOkResponseTypeHandler : ISpecificResponseTypeHandler
    {
        public bool CanHandle<TResult>(HttpResponseMessage responseMessage)
        {
            return responseMessage.IsSuccessStatusCode.IsFalse();
        }

        public async Task<TResult> HandleAsync<TResult>(HttpResponseMessage responseMessage,
                                                        HttpMethod httpMethod,
                                                        HttpClient httpClient,
                                                        string url)
        {
            // Safety first this method can be called without CanHandle check !
            if (CanHandle<TResult>(responseMessage).IsFalse())
            {
                throw new ProblemDetailsException("NotOkResponseTypeHandler was called without checking CanHandle.",
                                                  $"The response type handler for type: {typeof(TResult).Name} is not supported",
                                                  ("SupportedTypes", "NotOk responses"));
            }

            var content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            var absoluteUrl = $"{httpClient.BaseAddress}{url}";
            throw new ProblemDetailsException(responseMessage.StatusCode,
                                              "Client call to endpoint was not successfull",
                                              $"The http call: {httpMethod.Method} {url} was not succesfull",
                                              ("HttpMethod", httpMethod.Method),
                                              ("Url", absoluteUrl),
                                              ("Error", content));
        }
    }
}
