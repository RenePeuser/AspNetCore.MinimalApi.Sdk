using System.CommandLine;
using System.CommandLine.Invocation;
using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;
using AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V2.GetAllToDos;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V2
{
    internal static class AddV2CommandBuilderExtension
    {
        internal static void AddV2CommandBuilder(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGetAllToDosCommandBuilder(configuration);
            services.AddSingletonIfNotExists<IToDoSubCommandBuilder, V2CommandBuilder>();
        }
    }

    internal sealed class V2CommandBuilder(IEnumerable<IV2SubCommandBuilder> v2SubCommandBuilders) : IToDoSubCommandBuilder
    {
        public Command Build()
        {
            var v2Command = new Command("v2", "Here comes the description for V2");
            v2SubCommandBuilders.ToList().ForEach(builder => v2Command.AddCommand(builder.Build()));
            return v2Command;
        }
    }
}