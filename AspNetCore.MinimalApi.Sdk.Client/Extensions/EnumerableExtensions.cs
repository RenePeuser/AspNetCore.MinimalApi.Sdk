using System.Web;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.Client
{
    internal static class EnumerableExtensions
    {
        internal static string ToQueryParams<T>(this IEnumerable<T>? parameters, string parameterName)
        {
            if (parameters.IsNull())
            {
                return string.Empty;
            }

            var result = parameters.Select(param =>
            {
                if (param is string stringParameter)
                {
                    return $"{parameterName}={HttpUtility.UrlEncode(stringParameter)}";
                }

                return $"{parameterName}={param}";
            });
            var flattenParams = result.Flatten("&");
            return flattenParams;
        }
    }
}
