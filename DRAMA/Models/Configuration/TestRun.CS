using System.Text.Json.Serialization;

namespace DRAMA.Models.Configuration;

[Serializable]
internal class TestRun
{
    [JsonPropertyName("Results Path")]
    internal string? ResultsPath { get; set; }

    // TODO: See What "stopAtFirstError" Does & Implement Option To Skip All Step Runs After A Failed Step Run
}
