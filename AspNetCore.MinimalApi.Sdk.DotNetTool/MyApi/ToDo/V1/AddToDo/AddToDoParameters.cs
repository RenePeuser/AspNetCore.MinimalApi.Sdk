namespace AspNetCore.MinimalApi.Sdk.DotNetTool.MyApi.ToDo.V1.AddToDo
{
    internal sealed record AddToDoParameters(string Json, FormatType Format, FileInfo? Output, string Token);
}