using System.Text.Json.Serialization;

namespace DRAMA.Models.Configuration;

[Serializable]
internal class Profile
{
    [JsonPropertyName("Test Run")]
    internal TestRun? TestRun { get; set; }

    [JsonPropertyName("System Under Test")]
    internal SystemUnderTest? SystemUnderTest { get; set; }

    [JsonPropertyName("Browser Driver")]
    internal BrowserDriver? BrowserDriver { get; set; }
}
