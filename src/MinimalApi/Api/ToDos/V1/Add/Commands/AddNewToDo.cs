using MinimalApi.Api.ToDos.V1.DbContext;
using MinimalApi.Api.ToDos.V1.GetAll;

namespace MinimalApi.Api.ToDos.V1.Add
{
    internal static class AddAddNewToDoExtension
    {
        internal static void AddAddNewToDo(this IServiceCollection services)
        {
            services.AddTodoRepository();

            services.AddSingleton<AddNewToDo>();
        }
    }

    internal sealed class AddNewToDo(TodoRepository todoRepository)
    {
        internal Todo Execute(Todo todo)
        {
            return todoRepository.Add(todo);
        }
    }
}
