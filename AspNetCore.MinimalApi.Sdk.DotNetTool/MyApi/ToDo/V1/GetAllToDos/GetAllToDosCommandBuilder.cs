using System.CommandLine;
using System.CommandLine.Invocation;
using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.GetAllToDos
{
    internal static class AddGetAllToDosCommandBuilderExtension
    {
        internal static void AddGetAllToDosCommandBuilder(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGetAllToDosHandler(configuration);
            services.AddGetAllToDosOptionsBuilder();
            services.AddSingletonIfNotExists<IV1SubCommandBuilder, GetAllToDosCommandBuilder>();
        }
    }

    internal sealed class GetAllToDosCommandBuilder(GetAllToDosHandler getAllToDosHandler, GetAllToDosOptionsBuilder optionsBuilder) : IV1SubCommandBuilder
    {
        public Command Build()
        {
            var command = new Command("getalltodos", "Here comes the description for GetAllToDos");
            optionsBuilder.Build().ToList().ForEach(option => command.AddOption(option));
            command.Handler = CommandHandler.Create<string>((token) => getAllToDosHandler.HandleAsync(new GetAllToDosParameters(token)));
            return command;
        }
    }
}