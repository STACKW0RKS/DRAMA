namespace DRAMA.Models.Configuration;

[JsonObject]
public class Profile
{
    [JsonProperty("Name")]
    public string? Name { get; set; }

    [JsonProperty("Test Run")]
    public TestRun? TestRun { get; set; }

    [JsonProperty("System Under Test")]
    public SystemUnderTest? SystemUnderTest { get; set; }
}
