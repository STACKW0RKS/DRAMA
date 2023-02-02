namespace REHEARSAL.StepDefinitions.BackEnd;

[Binding]
internal sealed class BackEndSteps : ProtoStep
{
    internal BackEndSteps(FeatureContext featureContext) : base(featureContext) { }

    private IDbCommand CreateCommand(string commandText)
    {
        IDbCommand command = FeatureContext.GetProperty<IDbConnection>("Database Connection").CreateCommand();
        command.CommandText = commandText;

        return command;
    }

    [Given]
    public void THE_DATABASE_CONNECTION_IS_OPEN()
        => Assert.AreEqual(ConnectionState.Open, FeatureContext.GetProperty<IDbConnection>("Database Connection").State, "Connection State");

    [Given(@"I DROP ANY ALREADY-EXISTING ""(.*)"" TABLE")]
    public void I_DROP_ANY_ALREADY_EXISTING_P01_TABLE(string tableName)
        => CreateCommand($"DROP TABLE IF EXISTS {tableName}").ExecuteNonQuery();

    [When(@"I CREATE A NEW ""(.*)"" TABLE")]
    public void I_CREATE_A_NEW_P01_TABLE(string tableName)
        => CreateCommand($"CREATE TABLE IF NOT EXISTS {tableName} (TIMESTAMP VARCHAR(255), LANGUAGE VARCHAR(255), TRANSLATION VARCHAR(255))").ExecuteNonQuery();

    [When(@"I INSERT DATA INTO THE ""(.*)"" TABLE")]
    public void I_INSERT_DATA_INTO_THE_P01_TABLE(string tableName, Table table)
    {
        foreach (TableRow row in table.Rows)
        {
            string timestamp = DateTime.Now.ToString("dd.MM.yyyy @ HH:mm:ss.fff");
            string language = row.Values.First();
            string translation = row.Values.Skip(1).Take(1).Single();

            CreateCommand($"INSERT INTO {tableName} VALUES ('{timestamp}', '{language}', '{translation}')").ExecuteNonQuery();
        }
    }

    [Then(@"I CONFIRM THAT THE ""(.*)"" TABLE HAS ""(.*)"" ROWS")]
    public void I_CONFIRM_THAT_THE_P01_TABLE_HAS_P02_ROWS(string tableName, int rowCount)
    {
        DataTable results = new();
        results.Load(CreateCommand($"SELECT * FROM {tableName}").ExecuteReader());

        Assert.AreEqual(rowCount, results.Rows.Count, "Row Count");
    }
}
