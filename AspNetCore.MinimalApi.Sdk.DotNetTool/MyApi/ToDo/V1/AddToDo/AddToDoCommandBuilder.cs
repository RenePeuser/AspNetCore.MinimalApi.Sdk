using System.CommandLine;
using System.CommandLine.Invocation;
using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

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
            command.Handler = CommandHandler.Create<string, string>((json, token) => addToDoHandler.HandleAsync(new AddToDoParameters(json, token)));
            return command;
        }
    }
}