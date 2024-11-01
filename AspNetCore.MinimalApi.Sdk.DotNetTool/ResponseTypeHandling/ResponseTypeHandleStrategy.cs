using System.Collections.Immutable;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    internal static class AddResponseTypeHandleStrategyExtension
    {
        internal static void AddResponseTypeHandleStrategy(this IServiceCollection services)
        {
            services.AddNotOkResponseTypeHandler();
            services.AddJsonResponseTypeHandler();
            services.AddFileStreamResponseTypeHandler();
            services.AddByteArrayResponseTypeHandler();

            services.AddSingletonIfNotExists<ResponseTypeHandleStrategy>();
        }
    }

    internal interface ISpecificResponseTypeHandler
    {
        bool CanHandle<TResult>(HttpResponseMessage responseMessage);

        Task<TResult> HandleAsync<TResult>(HttpResponseMessage responseMessage,
                                           HttpMethod httpMethod,
                                           HttpClient httpClient,
                                           string url);
    }

    internal sealed class ResponseTypeHandleStrategy(IEnumerable<ISpecificResponseTypeHandler> responseHandlers)
    {
        public Task<TResult> HandleAsync<TResult>(HttpResponseMessage responseMessage,
                                                  HttpMethod httpMethod,
                                                  HttpClient httpClient,
                                                  string url)
        {
            var matchingResponseHandlers = responseHandlers.Where(handler => handler.CanHandle<TResult>(responseMessage)).ToImmutableList();
            if (matchingResponseHandlers.IsEmpty())
            {
                throw new ProblemDetailsException("No response handler was found to handle expected response",
                                                  $"No response handler was found to handle expected response type: '{typeof(TResult).Name}'",
                                                  ("Response type", typeof(TResult).Name));
            }

            if (matchingResponseHandlers.Count > 1)
            {
                throw new ProblemDetailsException("More than one reponse handlers was found for expected response type",
                                                  $"For response type: '{typeof(TResult).Name}' {matchingResponseHandlers.Count} response handler was found",
                                                  ("Response type", typeof(TResult).Name),
                                                  ("ResponseHandlers", matchingResponseHandlers.Select(handler => handler.GetType().Name).ToImmutableList()));
            }

            var result = matchingResponseHandlers[0].HandleAsync<TResult>(responseMessage, httpMethod, httpClient, url);
            return result;
        }
    }
}