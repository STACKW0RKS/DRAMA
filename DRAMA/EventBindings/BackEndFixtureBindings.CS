using System.Data;
using System.Data.Common;
using DRAMA.Extensions.Specflow;
using Microsoft.Data.Sqlite;
using Oracle.ManagedDataAccess.Client;

namespace DRAMA.EventBindings;

[Binding]
public class BackEndFixtureBindings
{
    private ExtentTest CurrentScenario;

    [BeforeFeature("Back-End", Order = 0)]
    public static void BeforeBackEndFeature(TestThreadContext testThreadContext)
    {
        DbConnectionStringBuilder connectionStringBuilder = new()
        { ConnectionString = testThreadContext.GetTestRunProfile().SystemUnderTest?.BackEnd?.ConnectionString };

        dynamic databaseConnection = testThreadContext.GetTestRunProfile().SystemUnderTest?.BackEnd?.DatabaseEngine?.ToTextValue() switch
        {
            "Oracle" => new OracleConnection(connectionStringBuilder.ConnectionString),
            "SQLite" => new SqliteConnection(connectionStringBuilder.ConnectionString)
        };

        if (Z34.TestRunProfile.Contains("SQLite"))
            Z34.ConnectionStringBuilder = new SqliteConnectionStringBuilder(configBindings.ConnectionString);

        if (Z34.TestRunProfile.Contains("Oracle"))
            Z34.ConnectionStringBuilder = configBindings.ConnectionString; // ¯\_(ヅ)_/¯

        if (Z34.TestRunProfile.Contains("SQLite"))
            Z34.DatabaseConnection = new SqliteConnection(Z34.ConnectionStringBuilder.ConnectionString);

        if (Z34.TestRunProfile.Contains("Oracle"))
            Z34.DatabaseConnection = new OracleConnection(Z34.ConnectionStringBuilder); // ¯\_(ヅ)_/¯
    }



    [AfterFeature("Back-End", Order = 0)]
    public static async Task AfterBackEndFeature()
    {
        if (Z34.DatabaseConnection is not null) await Z34.DatabaseConnection.CloseAsync();
    }
}
