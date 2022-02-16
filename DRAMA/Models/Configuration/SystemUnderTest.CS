namespace DRAMA.Models.Configuration;

[JsonObject]
public class SystemUnderTest
{
    [JsonProperty("Host")]
    public string? Host { get; set; }

    [JsonProperty("Protocol")]
    public string? Protocol { get; set; }

    [JsonProperty("Port")]
    public int? Port { get; set; }

    [JsonProperty("Origin")]
    public string? Origin { get; set; }

    [JsonProperty("Connection String")]
    public string? ConnectionString { get; set; }
}
