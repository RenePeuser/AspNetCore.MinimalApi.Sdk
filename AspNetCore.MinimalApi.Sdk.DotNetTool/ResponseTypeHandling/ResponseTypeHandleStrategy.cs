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
        Task<TResult> HandleAsync<TResult>(HttpResponseMessage responseMessage, HttpMethod httpMethod, HttpClient httpClient, string url);
    }

    internal sealed class ResponseTypeHandleStrategy(IEnumerable<ISpecificResponseTypeHandler> handlers)
    {
        internal Task<TResult> HandleAsync<TResult>(HttpResponseMessage responseMessage, HttpMethod httpMethod, HttpClient httpClient, string url)
        {
            var responseHandlers = handlers.Where(handler => handler.CanHandle<TResult>(responseMessage)).ToImmutableList();
            if (responseHandlers.IsEmpty())
            {
                throw new ProblemDetailsException("No response handler was found to handle expected response", $"No response handler was found to handle expected response type: '{typeof(TResult).Name}'", ("Response type", typeof(TResult).Name));
            }

            if (responseHandlers.Count > 1)
            {
                throw new ProblemDetailsException("More than one reponse handlers was found for expected response type", $"For response type: '{typeof(TResult).Name}' {responseHandlers.Count} response handler was found", ("Response type", typeof(TResult).Name), ("ResponseHandlers", responseHandlers.Select(handler => handler.GetType().Name).ToImmutableList()));
            }

            var result = responseHandlers[0].HandleAsync<TResult>(responseMessage, httpMethod, httpClient, url);
            return result;
        }
    }
}
