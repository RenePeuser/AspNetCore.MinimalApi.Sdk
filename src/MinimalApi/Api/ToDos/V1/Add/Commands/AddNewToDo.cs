using MinimalApi.Api.ToDos.V1.GetAll;

namespace MinimalApi.Api.ToDos.V1.Add
{
    internal static class AddAddNewToDoExtension
    {
        internal static void AddAddNewToDo(this IServiceCollection services)
        {
            services.AddSingleton<AddNewToDo>();
        }
    }

    internal sealed class AddNewToDo
    {
        internal Todo Execute(Todo todo)
        {
            return todo;
        }
    }
}
