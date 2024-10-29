using System.CommandLine;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Configuration
{
    internal interface IConfigurationSubCommandBuilder
    {
        Command Build();
    }
}
