using System.Data.Common;
using System.Data.SqlClient;
using DRAMA.Contexts;
using DRAMA.Enumerations;
using DRAMA.Extensions.Specflow;
using Microsoft.Data.Sqlite;
using Oracle.ManagedDataAccess.Client;

namespace DRAMA.EventBindings;

[Binding]
public class BackEndFixtureBindings
{
    [BeforeFeature("Back-End", Order = 0)]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        BackEnd? configuration = TestRunContext.Profile.SystemUnderTest?.BackEnd;

        DbConnectionStringBuilder connectionStringBuilder = new()
        { ConnectionString = configuration?.ConnectionString };

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

        featureContext.SetProperty("Database Connection", databaseConnection); // TODO: Create Dedicated Extension Method For This
    }

    [AfterFeature("Back-End", Order = 0)]
    public static void AfterFeature(FeatureContext featureContext)
    {
        // TODO: Implement This
        // if (connection is not null) await connection.CloseAsync();
    }
}
