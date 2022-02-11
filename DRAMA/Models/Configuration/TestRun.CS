using System.Text.Json.Serialization;

namespace DRAMA.Models.Configuration;

[Serializable]
internal class TestRun
{
    [JsonPropertyName("Results Path")]
    internal string? ResultsPath { get; set; }
}
