namespace MinimalApi.Api.ToDos.V1.GetAll
{
    internal static class AddGetAllToDosQueryExtension
    {
        internal static void AddGetAllToDosQuery(this IServiceCollection services)
        {
            services.AddSingleton<GetAllToDosQuery>();
        }
    }

    internal sealed class GetAllToDosQuery
    {
        private readonly Todo[] sampleTodos =
        {
            new()
            {
                Notes = new Notes { Hint = "Cool stuff", Key = "CS" },
                DueBy = DateOnly.FromDateTime(DateTime.Now),
                Id = 1,
                IsComplete = true,
                Title = "Walk with the dog"
            }
        };

        internal IEnumerable<Todo> Execute()
        {
            return sampleTodos;
        }
    }
}
