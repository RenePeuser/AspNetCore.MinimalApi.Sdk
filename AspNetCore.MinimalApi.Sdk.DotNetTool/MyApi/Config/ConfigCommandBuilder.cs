using System.CommandLine;
using Extensions.Pack;
using AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Config.Get;
using AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Config.Set;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Config
{
    internal static class AddConfigCommandBuilderExtension
    {
        internal static void AddConfigCommandBuilder(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGetCommandBuilder(configuration);
            services.AddSetCommandBuilder(configuration);
            services.AddSingletonIfNotExists<IMyApiSubCommandBuilder, ConfigCommandBuilder>();
        }
    }

    internal sealed class ConfigCommandBuilder(IEnumerable<IConfigSubCommandBuilder> configSubCommandBuilders) : IMyApiSubCommandBuilder
    {
        public Command Build()
        {
            var configCommand = new Command("config", "Command to handle the configuration for your dotnet tool. Known as appsettings");
            configSubCommandBuilders.ToList().ForEach(builder => configCommand.AddCommand(builder.Build()));
            return configCommand;
        }
    }
}
