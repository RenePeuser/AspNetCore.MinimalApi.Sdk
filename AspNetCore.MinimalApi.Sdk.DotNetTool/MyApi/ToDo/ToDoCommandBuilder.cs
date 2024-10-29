using System.CommandLine;
using Extensions.Pack;
using AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1;
using AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V2;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo
{
    internal static class AddToDoCommandBuilderExtension
    {
        internal static void AddToDoCommandBuilder(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddV1CommandBuilder(configuration);
            services.AddV2CommandBuilder(configuration);
            services.AddSingletonIfNotExists<IMyApiSubCommandBuilder, ToDoCommandBuilder>();
        }
    }

    internal sealed class ToDoCommandBuilder(IEnumerable<IToDoSubCommandBuilder> todoSubCommandBuilders) : IMyApiSubCommandBuilder
    {
        public Command Build()
        {
            var todoCommand = new Command("todo", "Here comes the description for ToDo");
            todoSubCommandBuilders.ToList().ForEach(builder => todoCommand.AddCommand(builder.Build()));
            return todoCommand;
        }
    }
}