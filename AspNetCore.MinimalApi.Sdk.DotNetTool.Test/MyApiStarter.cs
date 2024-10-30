using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using AspNetCore.Simple.MsTest.Sdk;
using Microsoft.Extensions.DependencyInjection;
using Extensions.Pack;
using System.Text.RegularExpressions;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.Test
{
    internal static class AddMyAppStarterExtension
    {
        internal static void AddMyAppStarter(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<MyAppStarter>();
        }
    }

    internal sealed record CliRunResult(string Output,
                                    int ExitCode);

    public partial class MyAppStarter
    {
        internal async Task<CliRunResult> StartAsync(string[] consoleArguments)
        {
            var originalConsoleOut = Console.Out;
            
            await using var sw = new StringWriter();
            var strings = consoleArguments;
            var consoleCall = strings.Flatten(" ");
            Console.WriteLine(consoleCall);
            Console.SetOut(sw);
            var exitCode = await Program.Main(strings).ConfigureAwait(false);
            var output = sw.ToString();
            
            Console.SetOut(originalConsoleOut);
            
            return new CliRunResult(output, exitCode);
        }

        internal Task<string> AssertRunAsync(string cliCall)
        {
            return AssertRunAsync(cliCall, string.Empty, string.Empty);
        }
        
        internal Task<string> AssertRunAsync(string cliCall,
                                             string expectedOutput,
                                             [CallerArgumentExpression(nameof(expectedOutput))] string expectedResultName = "")
        {
            var parameters = cliCall.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (cliCall.Contains('{', StringComparison.OrdinalIgnoreCase))
            {
                var match = ConsoleArgsSplitterRegex().Match(cliCall);
                parameters = match.Groups.Values.Skip(1).Select(g => g.Value).ToArray();
            }

            return AssertRunAsync(parameters, expectedOutput, expectedResultName);
        }

        internal async Task<string> AssertRunAsync(string[] consoleArguments,
                                                   string expectedOutput,
                                                   [CallerArgumentExpression(nameof(expectedOutput))] string expectedResultName = "")
        {
            var result = await StartAsync(consoleArguments);

            // check if embedded resources
            if (expectedOutput.EndsWith(".txt", StringComparison.OrdinalIgnoreCase) ||
                expectedOutput.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                expectedOutput = EmbeddedFile.GetFileContentFrom(expectedOutput);
            }


            // Check if valid exit code
            Assert.AreEqual(0, result.ExitCode, result.Output);

            // if no expected result value was passed no further checks are needed
            if (expectedOutput.IsNullOrWhiteSpace())
            {
                return result.Output;
            }

            if (result.Output.Contains('{', StringComparison.OrdinalIgnoreCase))
            {
                var current = JsonNode.Parse(result.Output);
                var expected = JsonNode.Parse(expectedOutput);

                Assert.That.ObjectsAreEqual(expected, current, expectedResultParameterName: expectedResultName);
            }
            else
            {
                Assert.That.ObjectsAreEqual(expectedOutput, result.Output, expectedResultParameterName: expectedResultName);
            }

            return result.Output;
        }

        [GeneratedRegex(@"(\S+)\s+(\S+)\s+(\S+)\s+({.*})")]
        private static partial Regex ConsoleArgsSplitterRegex();
    }
}
