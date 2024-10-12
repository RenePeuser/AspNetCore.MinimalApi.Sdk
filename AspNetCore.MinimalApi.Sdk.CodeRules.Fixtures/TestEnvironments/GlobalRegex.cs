using System.Text.RegularExpressions;

namespace AspNetCore.MinimalApi.Sdk.CodeRules.Fixtures.TestEnvironments;

internal static partial class GlobalRegex
{
    [GeneratedRegex("([V])\\d")]
    internal static partial Regex VersionRegex();

    [GeneratedRegex(@"\.MapToApiVersion\((?<version>\d+)\)")]
    internal static partial Regex MapToApiVersionRegex();
}
