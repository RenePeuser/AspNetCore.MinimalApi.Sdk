using System.Runtime.CompilerServices;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    internal sealed class HttpCallHandler(HttpClient httpClient, ResponseTypeHandleStrategy responseHandleStrategy, HttpRequestMessageBuilder httpRequestMessageBuilder)
    {
        internal async Task<string> CallAsync(HttpMethod httpMethod, string url, object? payload, CancellationToken cancellationToken, [CallerArgumentExpression(nameof(payload))] string payloadParameterName = "")
        {
            var message = httpRequestMessageBuilder.BuildFrom(httpMethod, url, payload, payloadParameterName);
            var response = await httpClient.SendAsync(message, cancellationToken).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return content;
            }

            var absoluteUrl = $"{httpClient.BaseAddress}{url}";
            throw new ProblemDetailsException("Client call to endpoint was not successful", $"The http call: {httpMethod.Method} {url} was not successful", ("HttpMethod", httpMethod.Method), ("Url", absoluteUrl), ("Error", content));
        }

        internal async Task<TResult> CallAsync<TResult>(HttpMethod httpMethod, string url, object? payload, CancellationToken cancellationToken, [CallerArgumentExpression(nameof(payload))] string payloadParameterName = "")
        {
            var message = httpRequestMessageBuilder.BuildFrom(httpMethod, url, payload, payloadParameterName);
            var response = await httpClient.SendAsync(message, cancellationToken).ConfigureAwait(false);
            var result = await responseHandleStrategy.HandleAsync<TResult>(response, httpMethod, httpClient, url).ConfigureAwait(false);
            return result;
        }

        internal async Task CallAsync(Func<HttpClient, string, string, Task<HttpResponseMessage>> httpFunction, HttpMethod httpMethod, string url, string paylod)
        {
            var response = await httpFunction(httpClient, url, paylod);
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var absoluteUrl = $"{httpClient.BaseAddress}{url}";
            throw new ProblemDetailsException("Client call to endpoint was not successfull", $"The http call: {httpMethod.Method} {url} was not successful", ("HttpMethod", httpMethod.Method), ("Url", absoluteUrl), ("Error", content));
        }

        internal async Task<TResult> CallAsync<TResult>(Func<HttpClient, string, string, Task<HttpResponseMessage>> httpFunction, HttpMethod httpMethod, string url, string paylod)
        {
            var response = await httpFunction(httpClient, url, paylod);
            var result = await responseHandleStrategy.HandleAsync<TResult>(response, httpMethod, httpClient, url).ConfigureAwait(false);
            return result;
        }
    }
}