using System.Text.Json.Serialization;

namespace DRAMA.Models.Configuration;

[Serializable]
internal class BrowserDriver
{
    [JsonPropertyName("Browser")]
    internal string? Browser { get; set; }

    [JsonPropertyName("Browser Options")]
    internal BrowserTypeLaunchOptions? BrowserOptions { get; set; }
}
