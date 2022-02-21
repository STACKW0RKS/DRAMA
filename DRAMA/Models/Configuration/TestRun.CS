namespace DRAMA.Models.Configuration;

[JsonObject]
public class TestRun
{
    [Newtonsoft.Json.JsonIgnore]
    public string? Name { get; set; }

    [JsonProperty("Results Path")]
    public string? ResultsPath { get; set; }

    // TODO: See What "stopAtFirstError" Does & Implement Option To Skip All Step Runs After A Failed Step Run
}
