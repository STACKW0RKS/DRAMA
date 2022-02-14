namespace REHEARSAL.TEMP;

[Binding]
internal sealed class BackEndSteps : ProtoStep
{
    internal BackEndSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext) { }

    /*
    private readonly SqliteConnection Connection;
    private readonly SqliteCommand Script;

    internal SQLiteDatabaseSteps(FeatureContext featureContext) : base(featureContext)
    {
        Connection = Z34.DatabaseConnection;
        Script = Z34.DatabaseConnection.CreateCommand();
    }

    [Given]
    public static void GIVEN_THE_SQLITE_DATABASE_DOES_NOT_EXIST()
    {
        if (File.Exists(Z34.ConnectionStringBuilder.DataSource))
            File.Delete(Z34.ConnectionStringBuilder.DataSource);

        Assert.IsFalse(File.Exists(Z34.ConnectionStringBuilder.DataSource), "Database Exists");
    }

    [Given]
    public async Task GIVEN_I_BOOTSTRAP_AND_CONNECT_TO_THE_DATABASE()
    {
        await Connection.OpenAsync();

        Debug.WriteLine
        (
            Connection.State == ConnectionState.Open
                ? $"Connection State: {Connection.State}; Server Version: {Connection.ServerVersion};"
                : $"Connection State: {Connection.State}"
        );
    }

    [Given]
    public void GIVEN_I_CONFIRM_THAT_THE_CONNECTION_IS_ALIVE()
        => Assert.AreEqual(ConnectionState.Open, Connection.State, "Connection State");

    [When(@"I CREATE A ""(.*)"" TABLE")]
    public async Task WHEN_I_CREATE_A_STRING_TABLE(string table)
    {
        Script.CommandText = $"CREATE TABLE IF NOT EXISTS {table} (TIMESTAMP VARCHAR(255), LANGUAGE VARCHAR(255), ENTRY VARCHAR(255))";
        await Script.ExecuteNonQueryAsync();
    }

    [When(@"I INSERT ""(.*)"" AND ""(.*)"" INTO THE ""(.*)"" TABLE")]
    public async Task WHEN_I_INSERT_STRING_AND_STRING_INTO_THE_STRING_TABLE(string language, string entry, string table)
    {
        Script.CommandText = $"INSERT INTO {table} VALUES ('{DateTime.UtcNow:dd.MM.yyyy @ HH:mm:ss.fff}', '{language}', '{entry}')";
        await Script.ExecuteNonQueryAsync();
    }

    [Then(@"I CONFIRM THAT A ROW HAS BEEN CREATED IN THE ""(.*)"" TABLE")]
    public async Task THEN_I_CONFIRM_THAT_A_ROW_HAS_BEEN_CREATED_IN_THE_STRING_TABLE(string table)
    {
        Script.CommandText = $"SELECT COUNT(*) FROM {table}";
        Assert.AreEqual(1, await Script.ExecuteScalarAsync(), "Row Count");
    }

    [Then]
    public async Task THEN_I_CLOSE_THE_DATABASE_CONNECTION()
    {
        if (Z34.DatabaseConnection is not null) await Z34.DatabaseConnection.CloseAsync();
    }
    */
}
