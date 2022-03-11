namespace DRAMA.EventBindings;

[Binding]
public sealed class BackEndFixtureBindings
{
    [BeforeFeature("Back-End", Order = 1)]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        BackEnd? configuration = TestRunContext.Profile.SystemUnderTest?.BackEnd;

        /*
        // TODO: Think Of How To Not Use A Dynamic Or An Object Type (https://stackoverflow.com/questions/981330/instantiate-an-object-with-a-runtime-determined-type)
        // Maybe Just Store To The Feature Context Straight From The Switch Statement
        object databaseConnection = configuration?.DatabaseEngine switch
        {
            // TODO: Add Support For All Database Engines Defined By The DatabaseEngine Class
            DatabaseEngine.MSSQLServer => new SqlConnection(connectionStringBuilder.ConnectionString),
            DatabaseEngine.Oracle => new OracleConnection(connectionStringBuilder.ConnectionString),
            DatabaseEngine.SQLite => new SqliteConnection(connectionStringBuilder.ConnectionString),
            _ => throw new NoMatchException($@"Invalid Database Engine Type ""{configuration?.DatabaseEngine}"" (Inspect The DatabaseEngine Type For Information On Supported Values)")
        };
        */

        DbProviderFactories.RegisterFactory("SQLite", /*SqlClientFactory.Instance*/ SqliteFactory.Instance);
        DbProviderFactory factory = DbProviderFactories.GetFactory("SQLite"/*configuration?.DatabaseEngine ?? throw new ConfigurationErrorsException("No DB Engine Defined")*/);

        IDbConnection databaseConnection = factory.CreateConnection() ?? throw new ConfigurationErrorsException("No DB Engine Defined 2");

        DbConnectionStringBuilder connectionStringBuilder = new() { ConnectionString = configuration?.ConnectionString };

        databaseConnection.ConnectionString = connectionStringBuilder.ConnectionString;

        featureContext.SetDatabaseConnection(databaseConnection);

        featureContext.GetDatabaseConnection().Open();
    }

    [AfterFeature("Back-End", Order = 1)]
    public static void AfterFeature(FeatureContext featureContext)
    {
        featureContext.GetDatabaseConnection().Close();
    }
}
