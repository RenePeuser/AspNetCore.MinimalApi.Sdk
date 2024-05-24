using System.Text.Json.Serialization;

namespace MinimalApi.Api.ToDos.V2.GetAll
{
    public static class AddToDoArraySerializerContextExtension
    {
        public static void AddToDoArraySerializerContext(this IServiceCollection services)
        {
            services.ConfigureHttpJsonOptions(options => options.SerializerOptions.TypeInfoResolverChain.Add(ToDoArraySerializerContextV2.Default));
        }
    }
    
    [JsonSerializable(typeof(MinimalApi.Api.ToDos.V2.GetAll.Todo[]))]
    internal partial class ToDoArraySerializerContextV2 : JsonSerializerContext
    {

    }
}
