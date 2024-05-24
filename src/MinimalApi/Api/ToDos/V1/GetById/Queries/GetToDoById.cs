using MinimalApi.Api.ToDos.V1.GetAll;

namespace MinimalApi.Api.ToDos.V1.GetById
{
    public static class AddGetToDoByIdExtension
    {
        public static void AddGetToDoById(this IServiceCollection services)
        {
            services.AddSingleton<GetToDoById>();
        }
    }
    
    internal class GetToDoById
    {
        private Todo[] sampleTodos = new Todo[] {
            new(1, "Walk the dog"),
            new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
            new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
            new(4, "Clean the bathroom"),
            new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
        };

        internal Todo? Execute(int id)
        {
            var result = sampleTodos.FirstOrDefault(t => t.Id == id);
            return result;
        }
    }
}
