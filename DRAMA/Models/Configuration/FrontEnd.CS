namespace DRAMA.Models.Configuration;

[JsonObject]
public class FrontEnd
{
    [JsonProperty("Host")]
    public string? Host { get; set; }

    [JsonProperty("Protocol")]
    public string? Protocol { get; set; }

    [JsonProperty("Port")]
    public int? Port { get; set; }

    [JsonProperty("Path")]
    public string? Path { get; set; }
}
