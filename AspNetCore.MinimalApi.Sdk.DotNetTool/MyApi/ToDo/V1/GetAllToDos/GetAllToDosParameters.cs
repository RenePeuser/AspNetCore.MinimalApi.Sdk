namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.GetAllToDos
{
    internal sealed record GetAllToDosParameters(FormatType Format, FileInfo? Output, string Token);
}