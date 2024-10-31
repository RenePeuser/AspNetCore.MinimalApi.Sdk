using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using AspNetCore.Simple.MsTest.Sdk;
using Microsoft.Extensions.DependencyInjection;
using Extensions.Pack;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestPlatform.Utilities;

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
        internal async Task<CliRunResult> RunAsync(string[] consoleArguments)
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

            var trimLineBreaks = output.TrimStart('\r').TrimStart('\n').TrimEnd('\n').TrimEnd('\r');

            return new CliRunResult(trimLineBreaks, exitCode);
        }

        internal Task<string> AssertRunAsync(string cliCall)
        {
            return AssertRunAsync(cliCall, string.Empty, string.Empty);
        }

        internal Task<T> AssertRunAsync<T>(string cliCall) where T : FileSystemInfo
        {
            return AssertRunAsync<T>(cliCall, string.Empty, string.Empty);
        }

        internal async Task<T> AssertRunAsync<T>(string cliCall,
                                           string expectedOutput,
                                           [CallerArgumentExpression(nameof(expectedOutput))] string expectedResultName = "") where T : FileSystemInfo
        {
            var result = await AssertRunAsync(cliCall, expectedOutput, expectedResultName);

            var exists = Path.Exists(result);
            if (exists)
            {
                var fileInfo = new FileInfo(result);
                return fileInfo.Cast<T>();
            }

            throw new InvalidOperationException("The console output is not a file path. Please check the output or your call to fix or change it.");
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
            var result = await RunAsync(consoleArguments);
            var currentOutput = result.Output;
            
            // check if embedded resources
            if (expectedOutput.EndsWith(".txt", StringComparison.OrdinalIgnoreCase) ||
                expectedOutput.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                expectedOutput = EmbeddedFile.GetFileContentFrom(expectedOutput);
            }


            // Check if valid exit code
            
            Assert.AreEqual(0, result.ExitCode, currentOutput);

            // if no expected result value was passed no further checks are needed
            if (expectedOutput.IsNullOrWhiteSpace())
            {
                return currentOutput;
            }

            // Its like magic
            if (Path.Exists(currentOutput))
            {
                currentOutput = await File.ReadAllTextAsync(currentOutput);
            }
            
            if (currentOutput.Contains('{', StringComparison.OrdinalIgnoreCase))
            {
                var current = JsonNode.Parse(currentOutput);
                var expected = JsonNode.Parse(expectedOutput);

                Assert.That.ObjectsAreEqual(expected, current, expectedResultParameterName: expectedResultName);
            }
            else
            {
                Assert.That.ObjectsAreEqual(expectedOutput, currentOutput, expectedResultParameterName: expectedResultName);
            }

            return result.Output;
        }

        [GeneratedRegex(@"(\S+)\s+(\S+)\s+(\S+)\s+({.*})")]
        private static partial Regex ConsoleArgsSplitterRegex();
    }
}
