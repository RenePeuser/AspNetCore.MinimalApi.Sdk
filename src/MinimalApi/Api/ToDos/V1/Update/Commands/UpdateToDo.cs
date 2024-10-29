using MinimalApi.Api.ToDos.V1.DbContext;
using MinimalApi.Api.ToDos.V1.GetAll;

namespace MinimalApi.Api.ToDos.V1.Update
{
    internal static class AddUpdateExtension
    {
        internal static void AddUpdateToDo(this IServiceCollection services)
        {
            services.AddSingleton<UpdateToDo>();
        }
    }

    internal sealed class UpdateToDo(TodoRepository todoRepository)
    {
        internal Todo Execute(int id, Todo todo)
        {
            return todoRepository.Update(id, todo);
        }
    }
}
