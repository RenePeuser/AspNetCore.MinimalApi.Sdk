using System.Collections.Immutable;
using System.Net;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    internal sealed class ProblemDetailsException : Exception
    {
        internal ProblemDetailsException(string title,
                                         params (string key, object value)[] extensions) : this(HttpStatusCode.InternalServerError, title, string.Empty, extensions)
        {
        }

        internal ProblemDetailsException(string title,
                                         string details,
                                         params (string key, object value)[] extensions) : this(HttpStatusCode.InternalServerError, title, details, extensions)
        {
        }

        internal ProblemDetailsException(HttpStatusCode statusCode,
                                         string title,
                                         string details,
                                         params (string key, object value)[] extensions) : this(statusCode.ToInt(), title, details, extensions.ToImmutableDictionary(item => item.key, item => item.value))
        {
        }

        internal ProblemDetailsException(int statusCode,
                                         string title,
                                         string details,
                                         params (string key, object value)[] extensions) : this(statusCode.ToInt(), title, details, extensions.ToImmutableDictionary(item => item.key, item => item.value))
        {
        }

        // i know this is evil with the conversion to immutable dictionary but a fast fix for now.

        internal ProblemDetailsException(int statusCode,
                                         string title,
                                         string details,
                                         IImmutableDictionary<string, object> errorDetails) : base(title)
        {
            var problemDetails = new ProblemDetails { Title = title.IsEmpty() ? null : title, Detail = details.IsEmpty() ? null : details, Status = statusCode };

            errorDetails.OrderBy(item => item.Key).ForEach(keyValue =>
            {
                var key = keyValue.Key.Split(" ").Select(value => value.FirstCharToUpper()).Flatten().FirstCharToLower();
                problemDetails.Extensions.Add(key, keyValue.Value);
            });

            ProblemDetails = problemDetails;
        }

        internal ProblemDetails ProblemDetails { get; }
    }
}
