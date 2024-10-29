using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    internal static class AddByteArrayResponseTypeHandlerExtension
    {
        internal static void AddByteArrayResponseTypeHandler(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<ISpecificResponseTypeHandler, ByteArrayResponseTypeHandler>();
        }
    }

    internal sealed class ByteArrayResponseTypeHandler : ISpecificResponseTypeHandler
    {
        public bool CanHandle<TResult>(HttpResponseMessage responseMessage)
        {
            return responseMessage.IsSuccessStatusCode && typeof(TResult) == typeof(byte[]);
        }

        public async Task<TResult> HandleAsync<TResult>(HttpResponseMessage responseMessage, HttpMethod httpMethod, HttpClient httpClient, string url)
        {
            // Safety first this method can be called without CanHandle check !
            if (CanHandle<TResult>(responseMessage).IsFalse())
            {
                throw new ProblemDetailsException("ByteArrayResponseTypeHandler was called without checking CanHandle.", $"The response type handler for type: {typeof(TResult).Name} is not supported", ("SupportedTypes", "byte[]"));
            }

            var result = await responseMessage.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            return (TResult)result.Cast<object>();
        }
    }
}