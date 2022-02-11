using System.Text.Json.Serialization;

namespace DRAMA.Models.Configuration;

[Serializable]
internal class SystemUnderTest
{
    [JsonPropertyName("Connection String")]
    internal string? ConnectionString { get; set; }

    [JsonPropertyName("Host")]
    internal string? Host { get; set; }

    [JsonPropertyName("Protocol")]
    internal string? Protocol { get; set; }

    [JsonPropertyName("Port")]
    internal int? Port { get; set; }

    [JsonPropertyName("Origin")]
    internal string? Origin { get; set; }
}
