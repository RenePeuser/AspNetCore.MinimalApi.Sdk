using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using AspNetCore.Simple.MsTest.Sdk;
using Microsoft.Extensions.DependencyInjection;
using Extensions.Pack;

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

    public class MyAppStarter
    {
        internal async Task<CliRunResult> StartAsync(string[] consoleArguments)
        {
            await using var sw = new StringWriter();
            var strings = consoleArguments;
            var consoleCall = strings.Flatten(" ");
            Console.WriteLine(consoleCall);
            Console.SetOut(sw);
            var exitCode = await Program.Main(strings).ConfigureAwait(false);
            var output = sw.ToString();

            return new CliRunResult(output, exitCode);
        }

        internal Task<string> AssertRunAsync(string cliCall,
                                             string expectedOutput,
                                             [CallerArgumentExpression(nameof(expectedOutput))] string expectedResultName = "")
        {
            return AssertRunAsync(cliCall.Split(" ").ToArray(), expectedOutput, expectedResultName);
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

            var current = JsonNode.Parse(result.Output);
            var expected = JsonNode.Parse(expectedOutput);

            Assert.That.ObjectsAreEqual(expected, current, expectedResultParameterName: expectedResultName);

            Console.SetOut(TextWriter.Null);
            
            Console.WriteLine(result.Output);

            return result.Output;
        }
    }
}
