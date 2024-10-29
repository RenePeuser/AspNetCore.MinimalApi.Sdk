using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    internal static class AddOutputFormatterExtension
    {
        internal static void AddOutputFormatter(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<OutputFormatter>();
        }
    }

    internal enum FormatType
    {
        Json,

        JsonIndented,

        JsonAsString,

        JsonIndentedAsString,
    }

    internal sealed class OutputFormatter(IEnumerable<IOutputFormatterStrategy> stringFormatterStrategies)
    {
        internal string Format(string value,
                               FormatType formatType)
        {
            // 1. Find the strategy that can handle the format type
            var strategies = stringFormatterStrategies.Where(s => s.CanHandle(formatType)).ToImmutableList();

            // 2. If we have no matched we throw problem details exception
            if (strategies.IsEmpty())
            {
                throw new ProblemDetailsException("No string formatter strategy was found",
                                                  $"No string formatter strategy can handle the specified format type: {formatType}",
                                                  ("Format", formatType),
                                                  ("String", value));
            }

            // 3. If we have more than one matched we throw problem details exception
            if (strategies.Count > 1)
            {
                throw new ProblemDetailsException("Multiple string formatter strategies were found",
                                                  $"Multiple string formatter strategies can handle the specified format type: {formatType}. Count: {strategies.Count}",
                                                  ("Format", formatType),
                                                  ("String", value),
                                                  ("Strategies", strategies.Select(s => s.GetType().Name)));
            }

            // 4. If we have one matched we format the value and return it
            return strategies[0].Format(value, formatType);
        }
    }

    internal interface IOutputFormatterStrategy
    {
        bool CanHandle(FormatType formatType);

        string Format(string value,
                      FormatType formatType);
    }

    public static class AddJsonFormatterExtension
    {
        public static void AddJsonFormatter(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<IOutputFormatterStrategy, JsonFormatterStrategy>();
        }
    }

    internal sealed class JsonFormatterStrategy : IOutputFormatterStrategy
    {
        public bool CanHandle(FormatType formatType)
        {
            return formatType == FormatType.Json;
        }

        public string Format(string value,
                             FormatType formatType)
        {
            // 1. Safety first this method can be called without CanHandle check !
            if (CanHandle(formatType))
            {
                throw new ProblemDetailsException("JsonFormatterStrategy was called without checking CanHandle.",
                                                  $"The string formatter strategy: {nameof(JsonFormatterStrategy)} can not process your request",
                                                  ("CanHandle", FormatType.Json));
            }

            // 2. Format incoming JSON string
            using var doc = JsonDocument.Parse(value);

            // 3. return the formatted json
            return doc.ToJson();
        }
    }

    public static class AddJsonIndentedFormatterExtension
    {
        public static void AddJsonIndentedFormatter(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<IOutputFormatterStrategy, JsonIndentedFormatterStrategy>();
        }
    }

    internal sealed class JsonIndentedFormatterStrategy : IOutputFormatterStrategy
    {
        public bool CanHandle(FormatType formatType)
        {
            return formatType == FormatType.JsonIndented;
        }

        public string Format(string value,
                             FormatType formatType)
        {
            // 1. Safety first this method can be called without CanHandle check !
            if (CanHandle(formatType))
            {
                throw new ProblemDetailsException("JsonFormatterStrategy was called without checking CanHandle.",
                                                  $"The string formatter strategy: {nameof(JsonIndentedFormatterStrategy)} can not process your request",
                                                  ("CanHandle", FormatType.JsonIndented));
            }

            // 2. Format incoming JSON string
            using var doc = JsonDocument.Parse(value);

            // 3. Return the formatted json
            return doc.ToJsonIntended();
        }
    }

    public static class AddJsonAsOutputFormatterExtension
    {
        public static void AddJsonAsOutputFormatter(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<IOutputFormatterStrategy, JsonAsOutputFormatterStrategy>();
        }
    }

    internal sealed class JsonAsOutputFormatterStrategy : IOutputFormatterStrategy
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = false,
            Converters = { new JsonStringEnumConverter() }
        };

        public bool CanHandle(FormatType formatType)
        {
            return formatType == FormatType.JsonAsString;
        }

        public string Format(string value,
                             FormatType formatType)
        {
            // 1. Safety first this method can be called without CanHandle check !
            if (CanHandle(formatType))
            {
                throw new ProblemDetailsException("JsonFormatterStrategy was called without checking CanHandle.",
                                                  $"The string formatter strategy: {nameof(JsonAsOutputFormatterStrategy)} can not process your request",
                                                  ("CanHandle", FormatType.JsonAsString));
            }


            // 2. Convert into object
            using var doc = JsonDocument.Parse(value);

            // 3. Serialize into json with specific options
            var json = JsonSerializer.Serialize(doc, _jsonSerializerOptions);

            // 4. Escape double quotes
            json = json.Replace("\"", "\\\"");

            // 5. Wrap it in double quotes to produce the desired output
            json = $"\"{json}\"";

            // 6. Return the formatted json
            return json;
        }
    }

    public static class AddJsonIndentedAsOutputFormatterExtension
    {
        public static void AddJsonIndentedAsOutputFormatter(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<IOutputFormatterStrategy, JsonIndentedAsOutputFormatterStrategy>();
        }
    }

    internal sealed class JsonIndentedAsOutputFormatterStrategy : IOutputFormatterStrategy
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public bool CanHandle(FormatType formatType)
        {
            return formatType == FormatType.JsonIndentedAsString;
        }

        public string Format(string value,
                             FormatType formatType)
        {
            // 1. Safety first this method can be called without CanHandle check !
            if (CanHandle(formatType))
            {
                throw new ProblemDetailsException("JsonFormatterStrategy was called without checking CanHandle.",
                                                  $"The string formatter strategy: {nameof(JsonIndentedAsOutputFormatterStrategy)} can not process your request",
                                                  ("CanHandle", FormatType.JsonIndentedAsString));
            }

            // 2. Convert into object
            using var doc = JsonDocument.Parse(value);

            // 3. Serialize into json with specific options
            var json = JsonSerializer.Serialize(doc, _jsonSerializerOptions);

            // 4. Escape double quotes
            json = json.Replace("\"", "\\\"");

            // 5. Wrap it in double quotes to produce the desired output
            json = $"\"{json}\"";

            // 6. Return the formatted json
            return json;
        }
    }
}
