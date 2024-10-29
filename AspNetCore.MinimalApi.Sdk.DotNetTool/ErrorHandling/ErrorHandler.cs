using System.CommandLine.Invocation;
using Extensions.Pack;

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
        internal async Task HandleErrorsAsync(InvocationContext context, Func<InvocationContext, Task> next)
        {
            try
            {
                await next(context).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var ex = FindMostSuitableException(e);
                if (ex is ProblemDetailsException problemDetailsException)
                {
                    consoleService.WriteError(problemDetailsException.ProblemDetails.ToJsonIntended());
                }
                else
                {
                    var problemDetails = new ProblemDetails
                    {
                        Title = $"Unexpected {ex.GetType().Name} occured",
                        Detail = ex.Message,
                        Status = 500
                    };
                    consoleService.WriteError(problemDetails.ToJsonIntended());
                }

                context.ResultCode = 1;
            }
        }

        private static Exception FindMostSuitableException(Exception exception)
        {
            if (exception is ProblemDetailsException)
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
