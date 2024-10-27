using System.CommandLine;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo
{
    internal interface IToDoSubCommandBuilder
    {
        Command Build();
    }
}