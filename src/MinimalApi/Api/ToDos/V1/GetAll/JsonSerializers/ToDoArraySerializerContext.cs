using System.Text.Json.Serialization;

namespace MinimalApi.Api.ToDos.V1.GetAll
{
    internal static class AddToDoArraySerializerContextExtension
    {
        internal static void AddToDoArraySerializerContext(this IServiceCollection services)
        {
            services.ConfigureHttpJsonOptions(options => options.SerializerOptions.TypeInfoResolverChain.Add(ToDoArraySerializerContext.Default));
        }
    }

    [JsonSerializable(typeof(Todo[]))]
    internal sealed partial class ToDoArraySerializerContext : JsonSerializerContext
    {
    }
}
