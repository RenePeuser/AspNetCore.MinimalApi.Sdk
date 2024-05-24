using System.Text.Json.Serialization;

namespace MinimalApi.Api.ToDos.V2.GetAll
{
        public static class AddToDoEnumerableSerializerContextExtension
        {
            public static void AddToDoEnumerableSerializerContext(this IServiceCollection services)
            {
                services.ConfigureHttpJsonOptions(options => options.SerializerOptions.TypeInfoResolverChain.Add(ToDoEnumerableSerializerContextV2.Default));
            }
        }

    [JsonSerializable(typeof(IEnumerable<MinimalApi.Api.ToDos.V2.GetAll.Todo>))]
    internal partial class ToDoEnumerableSerializerContextV2 : JsonSerializerContext
    {

    }
}
