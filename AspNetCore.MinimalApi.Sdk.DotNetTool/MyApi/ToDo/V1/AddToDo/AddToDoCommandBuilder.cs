using System.CommandLine;
using System.CommandLine.Invocation;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.AddToDo
{
    internal static class AddAddToDoCommandBuilderExtension
    {
        internal static void AddAddToDoCommandBuilder(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAddToDoHandler(configuration);
            services.AddAddToDoOptionsBuilder();
            services.AddAddToDoArgumentBuilder();
            services.AddSingletonIfNotExists<IV1SubCommandBuilder, AddToDoCommandBuilder>();
        }
    }

    internal sealed class AddToDoCommandBuilder(AddToDoHandler addToDoHandler, AddToDoOptionsBuilder optionsBuilder, AddToDoArgumentBuilder argumentBuilder) : IV1SubCommandBuilder
    {
        public Command Build()
        {
            var command = new Command("addtodo", "Here comes the description for AddToDo");
            optionsBuilder.Build().ToList().ForEach(option => command.AddOption(option));
            command.AddArgument(argumentBuilder.Build());
            command.Handler = CommandHandler.Create<string, FormatType, FileInfo?, string>((json, format, output, token) => addToDoHandler.HandleAsync(new AddToDoParameters(json, format, output, token)));
            return command;
        }
    }
}