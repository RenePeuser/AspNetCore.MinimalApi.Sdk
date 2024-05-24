namespace MinimalApi.Api.ToDos.V1.GetAll
{
    public record Todo(
        int Id,
        string? Title,
        DateOnly? DueBy = null,
        bool IsComplete = false);
}
