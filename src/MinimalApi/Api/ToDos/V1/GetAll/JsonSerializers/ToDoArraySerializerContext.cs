using System.Text.Json.Serialization;

namespace MinimalApi.Api.ToDos.V1.GetAll
{
    public static class AddToDoArraySerializerContextExtension
    {
        public static void AddToDoArraySerializerContext(this IServiceCollection services)
        {
            services.ConfigureHttpJsonOptions(options => options.SerializerOptions.TypeInfoResolverChain.Add(ToDoArraySerializerContext.Default));
        }
    }
    
    [JsonSerializable(typeof(Todo[]))]
    internal partial class ToDoArraySerializerContext : JsonSerializerContext
    {

    }
}
