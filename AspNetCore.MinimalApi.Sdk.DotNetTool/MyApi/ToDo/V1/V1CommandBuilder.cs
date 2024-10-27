using System.CommandLine;
using System.CommandLine.Invocation;
using Extensions.Pack;
using Microsoft.Extensions.DependencyInjection;
using AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.AddToDo;
using AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.DeleteToDoById;
using AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.GetAllToDos;
using AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.GetToDoById;
using AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.UpdateToDoById;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1
{
    internal static class AddV1CommandBuilderExtension
    {
        internal static void AddV1CommandBuilder(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAddToDoCommandBuilder(configuration);
            services.AddDeleteToDoByIdCommandBuilder(configuration);
            services.AddGetAllToDosCommandBuilder(configuration);
            services.AddGetToDoByIdCommandBuilder(configuration);
            services.AddUpdateToDoByIdCommandBuilder(configuration);
            services.AddSingletonIfNotExists<IToDoSubCommandBuilder, V1CommandBuilder>();
        }
    }

    internal sealed class V1CommandBuilder(IEnumerable<IV1SubCommandBuilder> v1SubCommandBuilders) : IToDoSubCommandBuilder
    {
        public Command Build()
        {
            var v1Command = new Command("v1", "Here comes the description for V1");
            v1SubCommandBuilders.ToList().ForEach(builder => v1Command.AddCommand(builder.Build()));
            return v1Command;
        }
    }
}