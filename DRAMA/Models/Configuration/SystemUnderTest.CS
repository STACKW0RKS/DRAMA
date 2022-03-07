namespace DRAMA.Models.Configuration;

[JsonObject]
public class SystemUnderTest
{
    [JsonProperty("Front-End")]
    public FrontEnd? FrontEnd { get; set; }

    [JsonProperty("API")]
    public API? API { get; set; }

    [JsonProperty("Back-End")]
    public BackEnd? BackEnd { get; set; }
}
