using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace tcortega.NubankClient.Utilities
{
    class NubankNullableStringEnumConverter<T> : JsonConverter<T>
    {
        private readonly JsonConverter<T> _converter;
        private readonly Type _underlyingType;

        public NubankNullableStringEnumConverter() : this(null) { }

        public NubankNullableStringEnumConverter(JsonSerializerOptions options)
        {
            // for performance, use the existing converter if available
            if (options != null)
            {
                _converter = (JsonConverter<T>)options.GetConverter(typeof(T));
            }

            // cache the underlying type
            _underlyingType = Nullable.GetUnderlyingType(typeof(T));
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(T).IsAssignableFrom(typeToConvert);
        }

        public override T Read(ref Utf8JsonReader reader,
            Type typeToConvert, JsonSerializerOptions options)
        {
            if (_converter != null)
            {
                return _converter.Read(ref reader, _underlyingType, options);
            }

            string value = reader.GetString();

            if (String.IsNullOrEmpty(value)) return default;

            if (!Enum.TryParse(_underlyingType, value.Replace("_", ""),
                ignoreCase: true, out object result))
            {
                throw new JsonException(
                    $"Unable to convert \"{value}\" to Enum \"{_underlyingType}\".");
            }

            return (T)result;
        }

        public override void Write(Utf8JsonWriter writer,
            T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString());
        }
    }
}
