using IPL.Logic.Typization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IPL.Helpers
{
    public class IPLDictionaryJsonConverter : JsonConverter<DictionaryValue>
    {
        public override DictionaryValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, DictionaryValue value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (var (k, val) in value.BaseDictionary)
                WriteValue(writer, k.AsString(), val);
            writer.WriteEndObject();
        }

        private void WriteValue(Utf8JsonWriter writer, string? key, IValue val)
        {
            if (key is null)
            {
                switch (val)
                {
                    case NumberValue number:
                        writer.WriteNumberValue(number.AsNumber());
                        break;
                    case StringValue str:
                        writer.WriteStringValue(str.AsString());
                        break;
                    case BoolValue boolean:
                        writer.WriteBooleanValue(boolean.AsBool());
                        break;
                    case ArrayValue array:
                        writer.WriteStartArray();
                        for (int i = 0; i < array.GetSize(); i++)
                            WriteValue(writer, null, array.Get(i));
                        writer.WriteEndArray();
                        break;
                    case DictionaryValue dictionary:
                        writer.WriteStartObject();
                        foreach (var (k, v) in dictionary.BaseDictionary)
                            WriteValue(writer, k.AsString(), v);
                        writer.WriteEndObject();
                        break;
                }
            }
            else
            {
                switch (val)
                {
                    case NumberValue number:
                        writer.WriteNumber(key, number.AsNumber());
                        break;
                    case StringValue str:
                        writer.WriteString(key, str.AsString());
                        break;
                    case BoolValue boolean:
                        writer.WriteBoolean(key, boolean.AsBool());
                        break;
                    case ArrayValue array:
                        writer.WriteStartArray(key);
                        for (int i = 0; i < array.GetSize(); i++)
                            WriteValue(writer, null, array.Get(i));
                        writer.WriteEndArray();
                        break;
                    case DictionaryValue dictionary:
                        writer.WriteStartObject(key);
                        foreach (var (k, v) in dictionary.BaseDictionary)
                            WriteValue(writer, k.AsString(), v);
                        writer.WriteEndObject();
                        break;
                }
            }
        }
    }
}
