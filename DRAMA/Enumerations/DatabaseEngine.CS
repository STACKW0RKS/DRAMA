namespace DRAMA.Enumerations;

/// <summary>
///     Used to create engine-specific database connections which support operations that target databases directly.
///     Example use cases include ETL/ELT operations or database seeding tasks.
/// </summary>
public static class DatabaseEngine
{
    public const string MSSQLServer = "Microsoft SQL Server";
    public const string MySQL = "MySQL";
    public const string Oracle = "Oracle";
    public const string PostgreSQL = "PostgreSQL";
    public const string SQLite = "SQLite";
}
