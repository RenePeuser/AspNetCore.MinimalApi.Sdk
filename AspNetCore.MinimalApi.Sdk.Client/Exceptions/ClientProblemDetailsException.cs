using System.Collections.Immutable;
using System.Net;
using Extensions.Pack;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.MinimalApi.Sdk.Client
{
    public class ClientProblemDetailsException : Exception
    {
        public ClientProblemDetailsException(string title,
                                             params (string key, object value)[] extensions) : this(HttpStatusCode.InternalServerError, title, string.Empty, extensions)
        {
        }

        public ClientProblemDetailsException(string title,
                                             string details,
                                             params (string key, object value)[] extensions) : this(HttpStatusCode.InternalServerError, title, details, extensions)
        {
        }

        public ClientProblemDetailsException(HttpStatusCode statusCode,
                                             string title,
                                             string details,
                                             params (string key, object value)[] extensions) : this(statusCode.ToInt(), title, details, extensions.ToImmutableDictionary(item => item.key, item => item.value))
        {
        }

        public ClientProblemDetailsException(int statusCode,
                                             string title,
                                             string details,
                                             params (string key, object value)[] extensions) : this(statusCode.ToInt(), title, details, extensions.ToImmutableDictionary(item => item.key, item => item.value))
        {
        }

        // i know this is evil with the conversion to immutable dictionary but a fast fix for now.
        public ClientProblemDetailsException(int statusCode,
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

        public ProblemDetails ProblemDetails { get; }
    }
}
