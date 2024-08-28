using MinimalApi.Api.ToDos.V1.GetAll;

namespace MinimalApi.Api.ToDos.V1.GetById
{
    internal static class AddGetToDoByIdExtension
    {
        internal static void AddGetToDoById(this IServiceCollection services)
        {
            services.AddSingleton<GetToDoById>();
        }
    }

    internal sealed class GetToDoById
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

        internal Todo? Execute(int id)
        {
            var result = sampleTodos.FirstOrDefault(t => t.Id == id);
            return result;
        }
    }
}
