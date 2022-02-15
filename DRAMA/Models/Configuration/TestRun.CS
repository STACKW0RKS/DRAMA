namespace DRAMA.Models.Configuration;

[JsonObject]
internal class TestRun
{
    [JsonProperty("Results Path")]
    internal string? ResultsPath { get; set; }

    // TODO: See What "stopAtFirstError" Does & Implement Option To Skip All Step Runs After A Failed Step Run
}
