namespace AspNetCore.MinimalApi.Sdk.Client.Api.ToDo.V2
{
    public record Todo(
        int Id,
        string? Title,
        DateOnly? DueBy = null,
        bool IsComplete = false);
}
