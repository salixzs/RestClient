using System;
using System.Buffers.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Salix.RestClient;

internal sealed class JsonObjectSerializerTimeSpanConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new InvalidOperationException($"Json converter for TimeStamp got token of type {reader.TokenType} instead of string. Check if you have proper types set and DateTime is serialized as string.");
        }

        var source = reader.ValueSpan;
        var result = Utf8Parser.TryParse(source, out TimeSpan tmpValue, out var bytesConsumed, 'c');

        // Note: Utf8Parser.TryParse will return true for invalid input so
        // long as it starts with an integer. Example: "2021-06-18" or
        // "1$$$$$$$$$$". We need to check bytesConsumed to know if the
        // entire source was actually valid.

        return result && source.Length == bytesConsumed
            ? tmpValue
            : throw new FormatException($"TimeStamp is in wrong format: {source.ToString()}");
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        const int maximumTimeSpanFormatLength = 25; // -ddddddd.hh:mm:ss.fffffff
        Span<byte> output = stackalloc byte[maximumTimeSpanFormatLength];
        var result = Utf8Formatter.TryFormat(value, output, out var bytesWritten, 'c');
        writer.WriteStringValue(output.Slice(0, bytesWritten));
    }
}
