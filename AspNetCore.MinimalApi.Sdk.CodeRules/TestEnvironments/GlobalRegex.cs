using System.Text.RegularExpressions;

namespace AspNetCore.MinimalApi.Sdk.CodeRules
{
    internal static partial class GlobalRegex
    {
        [GeneratedRegex("([V])\\d")]
        internal static partial Regex VersionRegex();

        [GeneratedRegex(@"<([^>]*)>")]
        internal static partial Regex GetGenericTypeRegex();

        [GeneratedRegex(@"\.MapToApiVersion\((?<version>\d+)\)")]
        internal static partial Regex MapToApiVersionRegex();

        [GeneratedRegex(@"\.HasApiVersion\(new ApiVersion\((?<version>[^\)]+)\)\)")]
        internal static partial Regex HasVersionRegex();
    }
}
