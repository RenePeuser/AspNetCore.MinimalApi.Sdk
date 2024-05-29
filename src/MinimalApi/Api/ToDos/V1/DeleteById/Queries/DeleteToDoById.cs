namespace MinimalApi.Api.ToDos.V1.DeleteById
{
    internal static class AddDeleteToDoByIdExtension
    {
        internal static void AddDeleteToDoById(this IServiceCollection services)
        {
            services.AddSingleton<DeleteToDoById>();
        }
    }

    internal sealed class DeleteToDoById
    {
        internal void Execute(int _)
        {

        }
    }
}
