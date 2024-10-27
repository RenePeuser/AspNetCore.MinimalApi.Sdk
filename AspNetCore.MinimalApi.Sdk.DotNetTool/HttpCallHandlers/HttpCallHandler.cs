using System.Runtime.CompilerServices;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    internal static class AddHttpCallHandlerExtension
    {
        internal static void AddHttpCallHandler(this IServiceCollection services)
        {
            services.AddResponseTypeHandleStrategy();
            services.AddHttpRequestMessageBuilder();
            services.AddHttpCallHandlerFactory();
            services.AddHttpClient();


            services.AddSingletonIfNotExists<HttpCallHandler>();
        }
    }

    public sealed class HttpCallHandler
    {
        private readonly HttpClient _httpClient;
        private readonly HttpRequestMessageBuilder _httpRequestMessageBuilder;
        private readonly ResponseTypeHandleStrategy _responseHandleStrategy;

        internal HttpCallHandler(HttpClient httpClient,
                                 ResponseTypeHandleStrategy responseHandleStrategy,
                                 HttpRequestMessageBuilder httpRequestMessageBuilder)
        {
            _httpClient = httpClient;
            _responseHandleStrategy = responseHandleStrategy;
            _httpRequestMessageBuilder = httpRequestMessageBuilder;
        }

        public async Task<string> CallAsync(HttpMethod httpMethod, string url, object? payload, CancellationToken cancellationToken, [CallerArgumentExpression(nameof(payload))] string payloadParameterName = "")
        {
            var message = _httpRequestMessageBuilder.BuildFrom(httpMethod, url, payload, payloadParameterName);
            var response = await _httpClient.SendAsync(message, cancellationToken).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return content;
            }

            var absoluteUrl = $"{_httpClient.BaseAddress}{url}";
            throw new ProblemDetailsException("Client call to endpoint was not successful", $"The http call: {httpMethod.Method} {url} was not successful", ("HttpMethod", httpMethod.Method), ("Url", absoluteUrl), ("Error", content));
        }

        public async Task<TResult> CallAsync<TResult>(HttpMethod httpMethod, string url, object? payload, CancellationToken cancellationToken, [CallerArgumentExpression(nameof(payload))] string payloadParameterName = "")
        {
            var message = _httpRequestMessageBuilder.BuildFrom(httpMethod, url, payload, payloadParameterName);
            var response = await _httpClient.SendAsync(message, cancellationToken).ConfigureAwait(false);
            var result = await _responseHandleStrategy.HandleAsync<TResult>(response, httpMethod, _httpClient, url).ConfigureAwait(false);
            return result;
        }

        public async Task CallAsync(Func<HttpClient, string, string, Task<HttpResponseMessage>> httpFunction, HttpMethod httpMethod, string url, string paylod)
        {
            var response = await httpFunction(_httpClient, url, paylod).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var absoluteUrl = $"{_httpClient.BaseAddress}{url}";
            throw new ProblemDetailsException("Client call to endpoint was not successfull", $"The http call: {httpMethod.Method} {url} was not successful", ("HttpMethod", httpMethod.Method), ("Url", absoluteUrl), ("Error", content));
        }

        public async Task<TResult> CallAsync<TResult>(Func<HttpClient, string, string, Task<HttpResponseMessage>> httpFunction, HttpMethod httpMethod, string url, string paylod)
        {
            var response = await httpFunction(_httpClient, url, paylod).ConfigureAwait(false);
            var result = await _responseHandleStrategy.HandleAsync<TResult>(response, httpMethod, _httpClient, url).ConfigureAwait(false);
            return result;
        }
    }
}
