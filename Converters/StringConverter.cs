// TS-04:31:03-04:55:35 created component to convert Number to String by means of JSON Serializer
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameStore.Frontend.Converters;

// TS-04:31:03-04:55:35 
// TS-04:31:03-04:55:35 public class StringConverter : JsonConverter<string?>
// TS-04:31:03-04:55:35 after coding the above line control dot on the StringConverter
// TS-04:31:03-04:55:35 and select implement abstract class to get the template generated
public class StringConverter : JsonConverter<string?>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // TS-04:31:03-04:55:35 throw new NotImplementedException();
        // TS-04:31:03-04:55:35 
        if (reader.TokenType == JsonTokenType.Number)
        {
            // TS-04:31:03-04:55:35 convert to int
            return reader.GetInt32().ToString();
        }
        return reader.GetString();
    }

    public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
    {
        // TS-04:31:03-04:55:35 throw new NotImplementedException();
        // TS-04:31:03-04:55:35 
        writer.WriteStringValue(value);
    }
}