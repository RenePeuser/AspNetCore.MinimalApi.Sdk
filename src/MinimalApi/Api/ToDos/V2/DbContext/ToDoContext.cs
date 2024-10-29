using Extensions.Pack;
using MinimalApi.Api.ToDos.V2.GetAll;

namespace MinimalApi.Api.ToDos.V2.DbContext
{
    internal static class AddTodoRepositoryExtension
    {
        internal static void AddTodoRepository(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<TodoRepository>();
        }
    }

    // I need a todo repository class which use internaly a simple List<Todo> to store the todos
    // - get all todo
    // - delete a todo by id
    // - get a todo by id
    // - update a todo by id
    internal class TodoRepository
    {
        private readonly List<Todo> _todos = new List<Todo>();

        public IEnumerable<Todo> GetAll()
        {
            return _todos;
        }

        public Todo Add(Todo todo)
        {
            _todos.Add(todo);
            return todo;
        }

        public Todo GetById(int id)
        {
            return _todos.First(t => t.Id == id);
        }

        public void Delete(int id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);

            if (todo != null)
            {
                _todos.Remove(todo);
            }
        }

        public Todo Update(int id, Todo todo)
        {
            var existingTodo = _todos.FirstOrDefault(t => t.Id == id);
            if (existingTodo != null)
            {
                _todos.Remove(existingTodo);
                _todos.Add(todo);
            }

            return todo;
        }
    }
}
