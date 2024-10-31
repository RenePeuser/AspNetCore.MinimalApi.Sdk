using System.CommandLine;
using System.CommandLine.Invocation;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Config.Get
{
    internal static class AddGetCommandBuilderExtension
    {
        internal static void AddGetCommandBuilder(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGetHandler(configuration);
            services.AddGetOptionsBuilder();
            services.AddSingletonIfNotExists<IConfigSubCommandBuilder, GetCommandBuilder>();
        }
    }

    internal sealed class GetCommandBuilder(GetHandler getHandler, GetOptionsBuilder optionsBuilder) : IConfigSubCommandBuilder
    {
        public Command Build()
        {
            var command = new Command("get", "Command to read the whole configuration for your dotnet tool. Known as appsettings");
            optionsBuilder.Build().ToList().ForEach(option => command.AddOption(option));
            command.Handler = CommandHandler.Create<FormatType, FileInfo?>((format, output) => getHandler.HandleAsync(new GetParameters(format, output)));
            return command;
        }
    }
}