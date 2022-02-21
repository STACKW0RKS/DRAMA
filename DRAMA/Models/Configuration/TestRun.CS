namespace DRAMA.Models.Configuration;

[JsonObject]
public class TestRun
{
    public TestRun()
        => Name = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}";

    [Newtonsoft.Json.JsonIgnore]
    public string Name { get; init; }

    [JsonProperty("Results Path")]
    public string? ResultsPath { get; set; }

    // TODO: See What "stopAtFirstError" Does & Implement Option To Skip All Step Runs After A Failed Step Run

    [Newtonsoft.Json.JsonIgnore]
    public Uri? Origin { get; set; } // TODO: I Think I Need To Stop Using This
}
