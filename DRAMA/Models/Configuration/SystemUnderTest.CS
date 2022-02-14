using Newtonsoft.Json;

namespace DRAMA.Models.Configuration;

[JsonObject]
internal class SystemUnderTest
{
    [JsonProperty("Connection String")]
    internal string? ConnectionString { get; set; }

    [JsonProperty("Host")]
    internal string? Host { get; set; }

    [JsonProperty("Protocol")]
    internal string? Protocol { get; set; }

    [JsonProperty("Port")]
    internal int? Port { get; set; }

    [JsonProperty("Origin")]
    internal string? Origin { get; set; }
}
