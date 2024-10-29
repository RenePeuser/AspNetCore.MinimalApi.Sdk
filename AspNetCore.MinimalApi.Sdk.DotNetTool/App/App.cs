using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    internal sealed class App(IServiceProvider serviceProvider)
    {
        internal async Task<int> RunAsync(string[] args)
        {
            // 1. Get needed service to invoke client generator. Important anything have to be handled by dependency injection
            var rootCommand = serviceProvider.GetRequiredService<MyApiCommandBuilder>().Build();
            var errorHandler = serviceProvider.GetRequiredService<ErrorHandler>();
            var dotNetCliArgumentFixer = serviceProvider.GetRequiredService<MyApiArgumentFixer>();

            // 2. Setup command line builder from microsoft cli sdk
            var commandLineBuilder = new CommandLineBuilder(rootCommand);
            commandLineBuilder.UseMiddleware(errorHandler.HandleErrorsAsync);
            commandLineBuilder.UseDefaults();
            var parser = commandLineBuilder.Build();

            // 3. We automatically add a version command
            var option = parser.Configuration.RootCommand.Options.Single(o => o.Name == "version").As<Option?>();
            option?.AddAlias("-v");

            // 4. Fix or update command parameter
            var fixedArgs = dotNetCliArgumentFixer.Fix(args);

            // 5. Here the cli sdk of microsoft will be invoked and manage any command execution
            var result = await parser.InvokeAsync(fixedArgs).ConfigureAwait(false);

            // 6. Return the result of the command execution
            return result;
        }
    }
}
