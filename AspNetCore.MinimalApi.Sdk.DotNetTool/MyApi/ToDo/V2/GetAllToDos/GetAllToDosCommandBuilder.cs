using System.CommandLine;
using System.CommandLine.Invocation;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V2.GetAllToDos
{
    internal static class AddGetAllToDosCommandBuilderExtension
    {
        internal static void AddGetAllToDosCommandBuilder(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGetAllToDosHandler(configuration);
            services.AddGetAllToDosOptionsBuilder();
            services.AddSingletonIfNotExists<IV2SubCommandBuilder, GetAllToDosCommandBuilder>();
        }
    }

    internal sealed class GetAllToDosCommandBuilder(GetAllToDosHandler getAllToDosHandler, GetAllToDosOptionsBuilder optionsBuilder) : IV2SubCommandBuilder
    {
        public Command Build()
        {
            var command = new Command("getalltodos", "Here comes the description for GetAllToDos");
            optionsBuilder.Build().ToList().ForEach(option => command.AddOption(option));
            command.Handler = CommandHandler.Create<FormatType, FileInfo?, string>((format, output, token) => getAllToDosHandler.HandleAsync(new GetAllToDosParameters(format, output, token)));
            return command;
        }
    }
}