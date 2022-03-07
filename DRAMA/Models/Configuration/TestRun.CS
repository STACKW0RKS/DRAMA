namespace DRAMA.Models.Configuration;

[JsonObject]
public class TestRun
{
    [Newtonsoft.Json.JsonIgnore]
    public string? Name { get; set; }

    [JsonProperty("Results Path")]
    public string? ResultsPath { get; set; }

    [JsonProperty("Stop Feature At First Error")]
    public bool? StopFeatureAtFirstError { get; set; }
}
