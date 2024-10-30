using System.CommandLine;
using System.CommandLine.Invocation;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.DeleteToDoById
{
    internal static class AddDeleteToDoByIdCommandBuilderExtension
    {
        internal static void AddDeleteToDoByIdCommandBuilder(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDeleteToDoByIdHandler(configuration);
            services.AddDeleteToDoByIdOptionsBuilder();
            services.AddDeleteToDoByIdArgumentBuilder();
            services.AddSingletonIfNotExists<IV1SubCommandBuilder, DeleteToDoByIdCommandBuilder>();
        }
    }

    internal sealed class DeleteToDoByIdCommandBuilder(DeleteToDoByIdHandler deleteToDoByIdHandler, DeleteToDoByIdOptionsBuilder optionsBuilder, DeleteToDoByIdArgumentBuilder argumentBuilder) : IV1SubCommandBuilder
    {
        public Command Build()
        {
            var command = new Command("deletetodobyid", "Here comes the description for DeleteToDoById");
            optionsBuilder.Build().ToList().ForEach(option => command.AddOption(option));
            command.AddArgument(argumentBuilder.Build());
            command.Handler = CommandHandler.Create<string, FormatType, FileInfo?, string>((json, format, output, token) => deleteToDoByIdHandler.HandleAsync(new DeleteToDoByIdParameters(json, format, output, token)));
            return command;
        }
    }
}