namespace DRAMA.Models.Configuration;

[JsonObject]
public class API // TODO: Implement API Testing (Look Into NBomber @ https://nbomber.com/)
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
