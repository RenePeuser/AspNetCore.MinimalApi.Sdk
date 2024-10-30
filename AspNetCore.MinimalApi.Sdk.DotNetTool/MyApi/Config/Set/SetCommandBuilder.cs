using System.CommandLine;
using System.CommandLine.Invocation;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Config.Set
{
    internal static class AddSetCommandBuilderExtension
    {
        internal static void AddSetCommandBuilder(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSetHandler(configuration);
            services.AddSetOptionsBuilder();
            services.AddSetArgumentBuilder();
            services.AddSingletonIfNotExists<IConfigSubCommandBuilder, SetCommandBuilder>();
        }
    }

    internal sealed class SetCommandBuilder(SetHandler setHandler, SetOptionsBuilder optionsBuilder, SetArgumentBuilder argumentBuilder) : IConfigSubCommandBuilder
    {
        public Command Build()
        {
            var command = new Command("set", "Command to set the whole configuration for your dotnet tool. Known as appsettings. To set the configuration call 'configuration get' change the settings you want to change and call 'configuration set' to set the whole json back");
            optionsBuilder.Build().ToList().ForEach(option => command.AddOption(option));
            command.AddArgument(argumentBuilder.Build());
            command.Handler = CommandHandler.Create<string, FormatType, FileInfo?>((json, format, output) => setHandler.HandleAsync(new SetParameters(json, format, output)));
            return command;
        }
    }
}
