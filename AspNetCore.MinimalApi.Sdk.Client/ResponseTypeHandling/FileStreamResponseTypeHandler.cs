using System.Net.Mime;
using Extensions.Pack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.Client
{
    internal static class AddFileStreamResponseTypeHandlerExtension
    {
        public static void AddFileStreamResponseTypeHandler(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<ISpecificResponseTypeHandler, FileStreamTypeHandler>();
        }
    }

    internal class FileStreamTypeHandler : ISpecificResponseTypeHandler
    {
        public async Task<TResult> HandleAsync<TResult>(HttpResponseMessage responseMessage,
                                                        HttpMethod httpMethod,
                                                        HttpClient httpClient,
                                                        string url)
        {
            // Safety first, check if we really can handle the result
            CanHandle<TResult>(responseMessage);

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
