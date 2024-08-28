/*
This is an example GlobalRegex. You should avoid using partial classes, because of complexity. In some parts you need to use partial classes (for example Regex).
Then it´s better to have only one class that is partial instead of having the regex spread all over the code.

using System.Text.RegularExpressions;

internal static partial class GlobalRegex
{
    [GeneratedRegex("([V])\\d")]
    internal static partial Regex VersionRegex();
}
*/


