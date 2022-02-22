namespace DRAMA.Models.Configuration;

[JsonObject]
public class SystemUnderTest
{
    [JsonProperty("Back-End")]
    public BackEnd? BackEnd { get; set; }

    [JsonProperty("API")]
    public API? API { get; set; }

    [JsonProperty("Front-End")]
    public FrontEnd? FrontEnd { get; set; }
}
