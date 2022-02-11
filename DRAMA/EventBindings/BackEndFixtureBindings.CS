using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using TechTalk.SpecFlow.Bindings;

namespace DRAMA.EventBindings;

[Binding]
public class BackEndFixtureBindings
{
    private ExtentTest CurrentScenario;

    [BeforeFeature("Back-End", Order = 0)]
    public static void BeforeBackEndFeature()
    {
        if (Z34.TestRunProfile.Contains("SQLite"))
            Z34.DatabaseConnection = new SqliteConnection(Z34.ConnectionStringBuilder.ConnectionString);

        if (Z34.TestRunProfile.Contains("Oracle"))
            Z34.DatabaseConnection = new OracleConnection(Z34.ConnectionStringBuilder); // ¯\_(ヅ)_/¯
    }

    [AfterStep("Back-End", Order = 0)]
    public void AfterBackEndStep(ScenarioContext scenarioContext)
    {
        StepDefinitionType stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType;

        if (scenarioContext.TestError is not null)
            _ = stepType switch
            {
                StepDefinitionType.Given => CurrentScenario
                    .CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text)
                    .Fail(scenarioContext.TestError.Message),
                StepDefinitionType.When => CurrentScenario
                    .CreateNode<When>(scenarioContext.StepContext.StepInfo.Text)
                    .Fail(scenarioContext.TestError.Message),
                StepDefinitionType.Then => CurrentScenario
                    .CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text)
                    .Fail(scenarioContext.TestError.Message),
                _ => throw new ArgumentOutOfRangeException(nameof(stepType), stepType, $"Invalid Step Type: {stepType}")
            };
    }

    [AfterFeature("Back-End", Order = 0)]
    public static async Task AfterBackEndFeature()
    {
        if (Z34.DatabaseConnection is not null) await Z34.DatabaseConnection.CloseAsync();
    }
}
