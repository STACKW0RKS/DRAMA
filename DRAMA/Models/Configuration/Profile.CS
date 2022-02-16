namespace DRAMA.Models.Configuration;

[JsonObject]
public class Profile
{
    [Newtonsoft.Json.JsonIgnore]
    public string? Name { get; set; }

    [JsonProperty("Test Run")]
    public TestRun? TestRun { get; set; }

    [JsonProperty("System Under Test")]
    public SystemUnderTest? SystemUnderTest { get; set; }

    [JsonProperty("Browser Driver")]
    public BrowserDriver? BrowserDriver { get; set; }
}
