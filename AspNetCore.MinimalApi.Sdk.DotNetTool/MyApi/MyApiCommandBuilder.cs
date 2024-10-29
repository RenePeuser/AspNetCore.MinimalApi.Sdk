using System.CommandLine;
using Extensions.Pack;
using AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi
{
    internal static class AddMyApiCommandBuilderExtension
    {
        internal static void AddMyApiCommandBuilder(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddToDoCommandBuilder(configuration);
            services.AddSingletonIfNotExists<MyApiCommandBuilder>();
        }
    }

    internal sealed class MyApiCommandBuilder(IEnumerable<IMyApiSubCommandBuilder> myapiSubCommandBuilders)
    {
        public Command Build()
        {
            var myapiCommand = new Command("myapi", "dotnet-MyApi");
            myapiSubCommandBuilders.ToList().ForEach(builder => myapiCommand.AddCommand(builder.Build()));
            return myapiCommand;
        }
    }
}