namespace AspNetCore.MinimalApi.Sdk.Client.Api.ToDo.V1
{
    public record Todo
    {
        public int Id { get; init; }
        public string? Title { get; init; }
        public Notes Notes { get; init; } = new();
        public DateOnly? DueBy { get; init; }
        public bool IsComplete { get; init; }
    }
}
