namespace MinimalApi.Api.ToDos.V1.GetAll
{
    public record Todo
    {
        public int Id { get; init; }
        public string? Title { get; init; }
        public Notes Notes { get; init; } = new Notes();
        public DateOnly? DueBy { get; init; }
        public bool IsComplete { get; init; }
    }


    public record Notes
    {
        public string Key { get; init; } = string.Empty;
        public string Hint { get; init; } = string.Empty;
    }
}
