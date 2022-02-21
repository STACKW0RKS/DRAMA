namespace DRAMA.Models.Configuration;

[JsonObject]
public class SystemUnderTest
{
    [JsonProperty("Back-End")]
    public BackEnd? BackEnd { get; set; }

    [JsonProperty("API")]
    public API? API { get; set; }

    [JsonProperty("Front-End")]
    public FrontEnd? FrontEnd { get; set; }
}

[JsonObject]
public class BackEnd
{
    [JsonProperty("Database Engine")]
    public string? DatabaseEngine { get; set; }

    [JsonProperty("Connection String")]
    public string? ConnectionString { get; set; }
}

[JsonObject]
public class API
{
    // TODO: Implement API Testing (Look Into NBomber @ https://nbomber.com/)
}

[JsonObject]
public class FrontEnd
{
    [JsonProperty("Host")]
    public string? Host { get; set; }

    [JsonProperty("Protocol")]
    public string? Protocol { get; set; }

    [JsonProperty("Port")]
    public int? Port { get; set; }

    [JsonProperty("Path")]
    public string? Path { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    public Uri? StartingURI { get; set; }
}

public static class DatabaseEngine
{
    public const string MariaDB = "MariaDB";
    public const string MSAzure = "Microsoft Azure SQL Database";
    public const string MSSQLServer = "Microsoft SQL Server";
    public const string MongoDB = "MongoDB";
    public const string MySQL = "MySQL";
    public const string Oracle = "Oracle";
    public const string PostgreSQL = "PostgreSQL";
    public const string SQLite = "SQLite";
}

// TODO: Restructure This Section
