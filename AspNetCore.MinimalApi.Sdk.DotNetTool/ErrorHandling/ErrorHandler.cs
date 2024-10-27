using System.CommandLine.Invocation;
using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    internal static class AddErrorHandlerExtension
    {
        internal static void AddErrorHandler(this IServiceCollection services)
        {
            services.AddConsoleService();
            services.AddSingletonIfNotExists<ErrorHandler>();
        }
    }

    internal sealed class ErrorHandler(ConsoleService consoleService)
    {
        public async Task HandleErrorsAsync(InvocationContext context, Func<InvocationContext, Task> next)
        {
            try
            {
                await next(context).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var ex = FindMostSuitableException(e);
                if (ex is MyApiException)
                {
                    consoleService.WriteError(ex.Message);
                }
                else
                {
                    consoleService.WriteError("An unhandled Error occurred:");
                    consoleService.WriteLine();
                    consoleService.WriteError(ex.ToString());
                }

                context.ResultCode = 1;
            }
        }

        private static Exception FindMostSuitableException(Exception exception)
        {
            if (exception is MyApiException)
            {
                return exception;
            }

            if (exception.InnerException != null)
            {
                return FindMostSuitableException(exception.InnerException);
            }

            return exception;
        }
    }
}