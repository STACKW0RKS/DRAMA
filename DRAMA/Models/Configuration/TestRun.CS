namespace DRAMA.Models.Configuration;

[JsonObject]
public class TestRun
{
    [JsonIgnore]
    public string? Name { get; set; }

    [JsonProperty("Results Path")]
    public string? ResultsPath { get; set; }

    [JsonProperty("Stop Feature At First Error")]
    public bool? StopFeatureAtFirstError { get; set; }

    [JsonProperty("Property Bag")]
    public Dictionary<string, object>? PropertyBag { get; set; }

    [JsonProperty("Debug Logging")]
    public bool? DebugLogging { get; set; }
}
