using System.Text.Json.Serialization;

namespace MinimalApi.Api.ToDos.V1.GetAll
{
    internal static class AddToDoEnumerableSerializerContextExtension
    {
        internal static void AddToDoEnumerableSerializerContext(this IServiceCollection services)
        {
            services.ConfigureHttpJsonOptions(options => options.SerializerOptions.TypeInfoResolverChain.Add(ToDoEnumerableSerializerContext.Default));
        }
    }

    [JsonSerializable(typeof(IEnumerable<Todo>))]
    internal sealed partial class ToDoEnumerableSerializerContext : JsonSerializerContext
    {
    }
}
