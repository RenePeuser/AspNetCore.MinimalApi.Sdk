using System.CommandLine;
using System.CommandLine.Invocation;
using AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Configuration.Get.Options;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Configuration.Get
{
    internal static class AddGetCommandBuilderCommandBuilderExtension
    {
        internal static void AddGetCommandBuilder(this IServiceCollection services)
        {
            services.AddGetCommandHandler();
            services.AddGetOptionsBuilder();

            services.AddSingletonIfNotExists<IConfigurationSubCommandBuilder, GetCommandBuilderCommandBuilder>();
        }
    }

    internal sealed class GetCommandBuilderCommandBuilder(GetCommandHandler getCommandHandler,
                                                          GetOptionsBuilder getOptionsBuilder) : IConfigurationSubCommandBuilder
    {
        public Command Build()
        {
            var getCommand = new Command("get", "Command to read the whole configuration for your dotnet tool. Known as appsettings");
            getOptionsBuilder.Build().ToList().ForEach(option => getCommand.AddOption(option));
            getCommand.Handler = CommandHandler.Create<FormatType, FileInfo?>((output, format) => getCommandHandler.HandleAsync(new GetParameters(output, format)));
            return getCommand;
        }
    }
}
