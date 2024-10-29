using MinimalApi.Api.ToDos.V1.DbContext;

namespace MinimalApi.Api.ToDos.V1.GetAll
{
    internal static class AddGetAllToDosQueryExtension
    {
        internal static void AddGetAllToDosQuery(this IServiceCollection services)
        {
            services.AddSingleton<GetAllToDosQuery>();
        }
    }

    internal sealed class GetAllToDosQuery(TodoRepository todoRepository)
    {
        internal IEnumerable<Todo> Execute()
        {
            return todoRepository.GetAll();
        }
    }
}
