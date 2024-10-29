using System.CommandLine;
using System.CommandLine.Invocation;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.UpdateToDoById
{
    internal static class AddUpdateToDoByIdCommandBuilderExtension
    {
        internal static void AddUpdateToDoByIdCommandBuilder(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddUpdateToDoByIdHandler(configuration);
            services.AddUpdateToDoByIdOptionsBuilder();
            services.AddUpdateToDoByIdArgumentBuilder();
            services.AddSingletonIfNotExists<IV1SubCommandBuilder, UpdateToDoByIdCommandBuilder>();
        }
    }

    internal sealed class UpdateToDoByIdCommandBuilder(UpdateToDoByIdHandler updateToDoByIdHandler, UpdateToDoByIdOptionsBuilder optionsBuilder, UpdateToDoByIdArgumentBuilder argumentBuilder) : IV1SubCommandBuilder
    {
        public Command Build()
        {
            var command = new Command("updatetodobyid", "Here comes the description for UpdateToDoById");
            optionsBuilder.Build().ToList().ForEach(option => command.AddOption(option));
            command.AddArgument(argumentBuilder.Build());
            command.Handler = CommandHandler.Create<string, string>((json, token) => updateToDoByIdHandler.HandleAsync(new UpdateToDoByIdParameters(json, token)));
            return command;
        }
    }
}