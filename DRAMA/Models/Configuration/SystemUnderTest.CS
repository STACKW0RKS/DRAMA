using DRAMA.Attributes;

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
    public DatabaseEngine? DatabaseEngine { get; set; }

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

    [JsonProperty("Origin")]
    public string? Origin { get; set; }
}

public enum DatabaseEngine
{
    [TextValue("MariaDB")] MariaDB,
    [TextValue("Microsoft Azure SQL Database")] MSAzure,
    [TextValue("Microsoft SQL Server")] MSSQLServer,
    [TextValue("MongoDB")] MongoDB,
    [TextValue("MySQL")] MySQL,
    [TextValue("Oracle")] Oracle,
    [TextValue("PostgreSQL")] PostgreSQL,
    [TextValue("SQLite")] SQLite
}
