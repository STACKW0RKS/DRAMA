namespace DRAMA.Models.Configuration;

[JsonObject]
public class BrowserDriver
{
    [JsonProperty("Browser")]
    public string? Browser { get; set; }

    [JsonProperty("Browser Options")]
    public BrowserTypeLaunchOptions? BrowserOptions { get; set; }
}
