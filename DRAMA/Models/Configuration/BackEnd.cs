namespace DRAMA.Models.Configuration;

[JsonObject]
public class BackEnd
{
    [JsonProperty("Database Engine")]
    public string? DatabaseEngine { get; set; }

    [JsonProperty("Connection String")]
    public string? ConnectionString { get; set; }
}
