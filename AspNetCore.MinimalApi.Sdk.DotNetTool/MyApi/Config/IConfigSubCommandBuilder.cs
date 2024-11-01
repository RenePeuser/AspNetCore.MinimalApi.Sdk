using System.CommandLine;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.Config
{
    internal interface IConfigSubCommandBuilder
    {
        Command Build();
    }
}