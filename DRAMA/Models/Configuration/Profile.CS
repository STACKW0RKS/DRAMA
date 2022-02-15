namespace DRAMA.Models.Configuration;

[JsonObject]
internal class Profile
{
    [JsonProperty("Test Run")]
    internal TestRun? TestRun { get; set; }

    [JsonProperty("System Under Test")]
    internal SystemUnderTest? SystemUnderTest { get; set; }

    [JsonProperty("Browser Driver")]
    internal BrowserDriver? BrowserDriver { get; set; }
}
