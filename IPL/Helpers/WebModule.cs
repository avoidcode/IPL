using IPL.Logic.Exceptions;
using IPL.Logic.Typization;
using System.Text.Json;

namespace IPL.Helpers
{
    public class WebModule
    {
        public static DictionaryValue Request(string url, string type, string body)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage msg;
            HttpContent content = new StringContent(body);
            switch (type)
            {
                case "GET":
                    msg = client.GetAsync(url).Result;
                    break;
                case "POST":
                    msg = client.PostAsync(url, content).Result;
                    break;
                case "PUT":
                    msg = client.PutAsync(url, content).Result;
                    break;
                case "PATCH":
                    msg = client.PatchAsync(url, content).Result;
                    break;
                case "DELETE":
                    msg = client.DeleteAsync(url).Result;
                    break;
                default:
                    throw new IPLRuntimeException($"No such HTTP request type: {type}");
            }
            string data = msg.Content.ReadAsStringAsync().Result;
            var result = new DictionaryValue();
            result.Set(new StringValue("status_code"), new NumberValue((int)msg.StatusCode));
            result.Set(new StringValue("content"), new StringValue(data));
            return result;
        }

        public static DictionaryValue ParseJson(string jsonObject)
        {
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonObject);
            var result = new DictionaryValue();
            foreach (KeyValuePair<string, object> d in data)
            {
                var key = new StringValue(d.Key);
                if (d.Value is null)
                {
                    result.Set(key, new StringValue("<null>"));
                    continue;
                }
                JsonElement element = (JsonElement)d.Value;
                if (element.ValueKind == JsonValueKind.Object)
                    result.Set(key, ParseJson(element.ToString()));
                else
                    result.Set(key, ParseJsonElement(element));
            }
            return result;
        }

        private static IValue ParseJsonElement(JsonElement element)
        {
            IValue result;
            switch (element.ValueKind)
            {
                case JsonValueKind.Number:
                    result = new NumberValue(element.GetDouble());
                    break;
                case JsonValueKind.String:
                    result = new StringValue(element.GetString());
                    break;
                case JsonValueKind.True:
                    result = new BoolValue(true);
                    break;
                case JsonValueKind.False:
                    result = new BoolValue(false);
                    break;
                case JsonValueKind.Array:
                    int len = element.GetArrayLength();
                    var arr = new ArrayValue(len);
                    for (int i = 0; i < len; i++)
                    {
                        JsonElement e = element[i];
                        if (e.ValueKind == JsonValueKind.Object)
                            arr.Set(i, ParseJson(e.GetRawText()));
                        else
                            arr.Set(i, ParseJsonElement(e));
                    }
                    result = arr;
                    break;
                default:
                    result = new StringValue("<null>");
                    break;
            }
            return result;
        }

        public static StringValue MakeJson(DictionaryValue dictionary)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new IPLDictionaryJsonConverter());
            return new StringValue(JsonSerializer.Serialize(dictionary, options));
        }
    }
}
