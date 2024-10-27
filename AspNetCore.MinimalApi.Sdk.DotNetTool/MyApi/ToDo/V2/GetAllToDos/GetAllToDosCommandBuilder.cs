using System.CommandLine;
using System.CommandLine.Invocation;
using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V2.GetAllToDos
{
    internal static class AddGetAllToDosCommandBuilderExtension
    {
        internal static void AddGetAllToDosCommandBuilder(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGetAllToDosHandler(configuration);
            services.AddGetAllToDosOptionsBuilder();
            services.AddGetAllToDosArgumentBuilder();
            services.AddSingletonIfNotExists<IV2SubCommandBuilder, GetAllToDosCommandBuilder>();
        }
    }

    internal sealed class GetAllToDosCommandBuilder(GetAllToDosHandler getAllToDosHandler, GetAllToDosOptionsBuilder optionsBuilder, GetAllToDosArgumentBuilder argumentBuilder) : IV2SubCommandBuilder
    {
        public Command Build()
        {
            var command = new Command("getalltodos", "Here comes the description for GetAllToDos");
            optionsBuilder.Build().ToList().ForEach(option => command.AddOption(option));
            command.AddArgument(argumentBuilder.Build());
            command.Handler = CommandHandler.Create<string, string>((json, token) => getAllToDosHandler.HandleAsync(new GetAllToDosParameters(json, token)));
            return command;
        }
    }
}