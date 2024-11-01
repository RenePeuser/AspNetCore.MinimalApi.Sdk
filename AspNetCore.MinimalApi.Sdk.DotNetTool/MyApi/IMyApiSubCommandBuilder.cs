using System.CommandLine;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi
{
    internal interface IMyApiSubCommandBuilder
    {
        Command Build();
    }
}