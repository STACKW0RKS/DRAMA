namespace DRAMA.Models.Configuration;

[JsonObject]
public class Integrations
{
    [JsonProperty("Azure DevOps")]
    public AzureDevOps? AzureDevOps { get; set; }
}

[JsonObject]
public class AzureDevOps
{
    [JsonProperty("Enabled")]
    public bool? Enabled { get; set; }

    [JsonProperty("Host")]
    public string? Host { get; set; }

    [JsonProperty("Project")]
    public string? Project { get; set; }

    [JsonProperty("Personal Access Token")]
    public string? PersonalAccessToken { get; set; }

    [JsonIgnore]
    public AzureDevOpsIntegration? Integration { get; set; }
}
