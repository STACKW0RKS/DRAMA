namespace DRAMA.Helpers.Configuration;

internal static class ConfigurationHelpers
{
    internal static IDbConnection CreateDatabaseConnection()
    {
        BackEnd? configuration = Contexts.TestRun.Profile.SystemUnderTest?.BackEnd;

        DbProviderFactory factory = configuration?.DatabaseEngine switch
        {
            DatabaseEngine.MSSQLServer => ((Func<DbProviderFactory>)(() =>
            {
                DbProviderFactories.RegisterFactory(DatabaseEngine.MSSQLServer, SqlClientFactory.Instance);

                return DbProviderFactories.GetFactory(DatabaseEngine.MSSQLServer);
            }))(),

            DatabaseEngine.MySQL => ((Func<DbProviderFactory>)(() =>
            {
                DbProviderFactories.RegisterFactory(DatabaseEngine.MySQL, MySqlClientFactory.Instance);

                return DbProviderFactories.GetFactory(DatabaseEngine.MySQL);
            }))(),

            DatabaseEngine.Oracle => ((Func<DbProviderFactory>)(() =>
            {
                DbProviderFactories.RegisterFactory(DatabaseEngine.Oracle, OracleClientFactory.Instance);

                return DbProviderFactories.GetFactory(DatabaseEngine.Oracle);
            }))(),

            DatabaseEngine.PostgreSQL => ((Func<DbProviderFactory>)(() =>
            {
                DbProviderFactories.RegisterFactory(DatabaseEngine.PostgreSQL, NpgsqlFactory.Instance);

                return DbProviderFactories.GetFactory(DatabaseEngine.PostgreSQL);
            }))(),

            DatabaseEngine.SQLite => ((Func<DbProviderFactory>)(() =>
            {
                DbProviderFactories.RegisterFactory(DatabaseEngine.SQLite, SqliteFactory.Instance);

                return DbProviderFactories.GetFactory(DatabaseEngine.SQLite);
            }))(),

            _ => throw new NoMatchException($@"Unsupported Database Engine ""{configuration?.DatabaseEngine}""")
        };

        IDbConnection databaseConnection = factory.CreateConnection() ?? throw new ConfigurationErrorsException("No Database Engine Defined");

        DbConnectionStringBuilder connectionStringBuilder = new() { ConnectionString = configuration?.ConnectionString };

        databaseConnection.ConnectionString = connectionStringBuilder.ConnectionString;

        return databaseConnection;
    }
}
