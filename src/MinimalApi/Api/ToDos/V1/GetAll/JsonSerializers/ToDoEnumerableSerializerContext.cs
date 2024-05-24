using System.Text.Json.Serialization;

namespace MinimalApi.Api.ToDos.V1.GetAll
{
    public static class AddToDoEnumerableSerializerContextExtension
    {
        public static void AddToDoEnumerableSerializerContext(this IServiceCollection services)
        {
            services.ConfigureHttpJsonOptions(options => options.SerializerOptions.TypeInfoResolverChain.Add(ToDoEnumerableSerializerContext.Default));
        }
    }

    [JsonSerializable(typeof(IEnumerable<Todo>))]
    internal partial class ToDoEnumerableSerializerContext : JsonSerializerContext
    {

    }
}
