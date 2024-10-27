using System.CommandLine;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V2
{
    internal interface IV2SubCommandBuilder
    {
        Command Build();
    }
}