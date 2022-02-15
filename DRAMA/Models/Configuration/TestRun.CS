namespace DRAMA.Models.Configuration;

[JsonObject]
public class TestRun
{
    public TestRun()
        => Name = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}";

    [JsonProperty("Results Path")]
    public string? ResultsPath { get; set; }

    // TODO: See What "stopAtFirstError" Does & Implement Option To Skip All Step Runs After A Failed Step Run

    public Uri? Origin { get; set; }

    public string Name { get; init; }
}
