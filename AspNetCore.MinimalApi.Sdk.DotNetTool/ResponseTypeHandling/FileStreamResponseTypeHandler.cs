using System.Net.Mime;
using Extensions.Pack;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    internal static class AddFileStreamResponseTypeHandlerExtension
    {
        internal static void AddFileStreamResponseTypeHandler(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<ISpecificResponseTypeHandler, FileStreamTypeHandler>();
        }
    }

    internal sealed class FileStreamTypeHandler : ISpecificResponseTypeHandler
    {
        public async Task<TResult> HandleAsync<TResult>(HttpResponseMessage responseMessage,
                                                        HttpMethod httpMethod,
                                                        HttpClient httpClient,
                                                        string url)
        {
            // Safety first this method can be called without CanHandle check !
            if (CanHandle<TResult>(responseMessage).IsFalse())
            {
                throw new ProblemDetailsException("FileStreamResponseTypeHandler was called without checking CanHandle.",
                                                  $"The response type handler for type: {typeof(TResult).Name} is not supported",
                                                  ("SupportedTypes", "FileStreamResult"));
            }

            var content = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var fileName = responseMessage.Content.Headers.ContentDisposition?.FileName ?? string.Empty;
            var fileStreamResult = new FileStreamResult(content, MediaTypeNames.Application.Octet) { FileDownloadName = fileName };
            return fileStreamResult.Cast<TResult>();
        }

        public bool CanHandle<TResult>(HttpResponseMessage responseMessage)
        {
            return responseMessage.IsSuccessStatusCode &&
                   typeof(TResult) == typeof(FileStreamResult);
        }
    }
}
