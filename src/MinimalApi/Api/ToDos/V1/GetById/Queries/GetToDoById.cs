using MinimalApi.Api.ToDos.V1.DbContext;
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

    internal sealed class GetToDoById(TodoRepository todoRepository)
    {
        internal Todo Execute(int id)
        {
            return todoRepository.GetById(id);
        }
    }
}
