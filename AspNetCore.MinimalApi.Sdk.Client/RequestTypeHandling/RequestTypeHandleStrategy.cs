using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.Client.RequestTypeHandling
{
    public static class AddRequestTypeHandleStrategyExtension
    {
        public static void AddRequestTypeHandleStrategy(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<RequestTypeHandleStrategy>();
        }
    }

    internal interface ISpecificRequestTypeHandler
    {
        bool CanHandle<TResult>(HttpResponseMessage responseMessage);

        Task<TResult> HandleAsync<TResult>(HttpResponseMessage responseMessage, HttpMethod httpMethod, HttpClient httpClient, string url);
    }

    internal class RequestTypeHandleStrategy
    {
        private readonly IEnumerable<ISpecificResponseTypeHandler> _responseHandlers;

        public RequestTypeHandleStrategy(IEnumerable<ISpecificResponseTypeHandler> responseHandlers)
        {
            _responseHandlers = responseHandlers;
        }

        public Task<TResult> HandleAsync<TResult>(HttpResponseMessage responseMessage, HttpMethod httpMethod, HttpClient httpClient, string url)
        {
            var responseHandlers = _responseHandlers.Where(handler => handler.CanHandle<TResult>(responseMessage)).ToImmutableList();
            if (responseHandlers.IsEmpty())
            {
                throw new ClientProblemDetailsException("No response handler was found to handle expected response",
                                                  $"No response handler was found to handle expected response type: '{typeof(TResult).Name}'",
                                                  ("Response type", typeof(TResult).Name));
            }

            if (responseHandlers.Count > 1)
            {
                throw new ClientProblemDetailsException("More than one reponse handlers was found for expected response type",
                                                  $"For response type: '{typeof(TResult).Name}' {responseHandlers.Count} response handler was found",
                                                  ("Response type", typeof(TResult).Name),
                                                  ("ResponseHandlers", responseHandlers.Select(handler => handler.GetType().Name).ToImmutableList()));
            }

            var result = responseHandlers[0].HandleAsync<TResult>(responseMessage, httpMethod, httpClient, url);
            return result;
        }
    }
}
