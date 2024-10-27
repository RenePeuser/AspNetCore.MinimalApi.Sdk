using System.CommandLine;
using System.CommandLine.Invocation;
using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.GetToDoById
{
    internal static class AddGetToDoByIdCommandBuilderExtension
    {
        internal static void AddGetToDoByIdCommandBuilder(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGetToDoByIdHandler(configuration);
            services.AddGetToDoByIdOptionsBuilder();
            services.AddGetToDoByIdArgumentBuilder();
            services.AddSingletonIfNotExists<IV1SubCommandBuilder, GetToDoByIdCommandBuilder>();
        }
    }

    internal sealed class GetToDoByIdCommandBuilder(GetToDoByIdHandler getToDoByIdHandler, GetToDoByIdOptionsBuilder optionsBuilder, GetToDoByIdArgumentBuilder argumentBuilder) : IV1SubCommandBuilder
    {
        public Command Build()
        {
            var command = new Command("gettodobyid", "Here comes the description for GetToDoById");
            optionsBuilder.Build().ToList().ForEach(option => command.AddOption(option));
            command.AddArgument(argumentBuilder.Build());
            command.Handler = CommandHandler.Create<string, string>((json, token) => getToDoByIdHandler.HandleAsync(new GetToDoByIdParameters(json, token)));
            return command;
        }
    }
}