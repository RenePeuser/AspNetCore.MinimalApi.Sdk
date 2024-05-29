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

    internal sealed class UpdateToDo
    {
        internal Todo Execute(int _, Todo todo)
        {
            return todo;
        }
    }
}
