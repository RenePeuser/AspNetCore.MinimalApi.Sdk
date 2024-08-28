using System.Text.RegularExpressions;

internal static partial class GlobalRegex
{
    [GeneratedRegex("([V])\\d")]
    internal static partial Regex VersionRegex();

    [GeneratedRegex(@"\.MapToApiVersion\((?<version>\d+)\)")]
    internal static partial Regex MapToApiVersionRegex();
}
