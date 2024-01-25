using System.Text.Json;
using System.Text.Json.Serialization;

namespace Auxilium.Dev.Lint;

internal class PackageManagerNameJsonConverter : JsonConverter<PackageManagerName>
{
    public override PackageManagerName? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => new(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, PackageManagerName value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.Value);
}
