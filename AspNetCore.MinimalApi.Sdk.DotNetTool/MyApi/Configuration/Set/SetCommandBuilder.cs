using System.CommandLine;
using System.CommandLine.Invocation;
using AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.AddToDo;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Configuration.Set
{
    internal static class AddSetCommandBuilderExtension
    {
        internal static void AddSetCommandBuilder(this IServiceCollection services)
        {
            services.AddSetCommandHandler();
            services.AddSetArgumentBuilder();

            services.AddSingletonIfNotExists<IConfigurationSubCommandBuilder, SetCommandBuilder>();
        }
    }

    internal sealed class SetCommandBuilder(SetCommandHandler setCommandHandler,
                                            SetArgumentBuilder setArgumentBuilder) : IConfigurationSubCommandBuilder
    {
        public Command Build()
        {
            var setCommand = new Command("set", "Command to set the whole configuration for your dotnet tool. Known as appsettings. To set the configuration call 'configuration get' change the settings you want to change and call 'configuration set' to set the whole json back");
            setCommand.AddArgument(setArgumentBuilder.Build());
            setCommand.Handler = CommandHandler.Create<string>((json) => setCommandHandler.HandleAsync(new GetParameters(json)));
            return setCommand;
        }
    }
}
