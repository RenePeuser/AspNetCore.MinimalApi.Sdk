using System.CommandLine;
using AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Configuration.Get;
using AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Configuration.Set;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Configuration
{
    internal static class AddConfigurationCommandBuilderExtension
    {
        internal static void AddConfigurationCommandBuilder(this IServiceCollection services)
        {
            services.AddGetCommandBuilder();
            services.AddSetCommandBuilder();

            services.AddSingletonIfNotExists<IMyApiSubCommandBuilder, ConfigurationCommandBuilder>();
        }
    }

    internal sealed class ConfigurationCommandBuilder(IEnumerable<IConfigurationSubCommandBuilder> myapiSubCommandBuilders) : IMyApiSubCommandBuilder
    {
        public Command Build()
        {
            var configurationCommand = new Command("configuration", "Command to handle the configuration for your dotnet tool. Known as appsettings");
            myapiSubCommandBuilders.ToList().ForEach(builder => configurationCommand.AddCommand(builder.Build()));
            return configurationCommand;
        }
    }
}
