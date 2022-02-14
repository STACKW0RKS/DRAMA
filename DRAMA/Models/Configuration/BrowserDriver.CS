using Newtonsoft.Json;

namespace DRAMA.Models.Configuration;

[JsonObject]
internal class BrowserDriver
{
    [JsonProperty("Browser")]
    internal string? Browser { get; set; }

    [JsonProperty("Browser Options")]
    internal BrowserTypeLaunchOptions? BrowserOptions { get; set; }
}
