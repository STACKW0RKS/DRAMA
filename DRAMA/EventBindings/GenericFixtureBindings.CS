using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using TechTalk.SpecFlow.Bindings;

namespace DRAMA.EventBindings;

[Binding]
public class GenericFixtureBindings
{
    private static ExtentReports Extent = new();
    private static List<ExtentTest> Features = new();
    private ExtentTest CurrentScenario;

    public SpecFlowEventBindings(FeatureContext featureContext, ScenarioContext scenarioContext) { }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        ConfigurationReader.SetFrameworkSettings(Z34.TestRunProfile);
        ExtentHtmlReporter reporter = new(Z34.TestResultsDirectory + Path.DirectorySeparatorChar);
        reporter.Config.EnableTimeline = true;
        reporter.Config.ReportName = Z34.TestRunName;
        reporter.Config.DocumentTitle = Z34.TestRunName;
        reporter.Config.Theme = Theme.Dark;
        Extent.AttachReporter(reporter);
    }

    [BeforeFeature]
    public static void BeforeFeature(FeatureContext featureContext)
        => Features.Add(Extent.CreateTest<Feature>(featureContext.FeatureInfo.Title));

    [BeforeScenario]
    public void BeforeScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        => CurrentScenario = Features.Single(feature => feature.Model.Name == featureContext.FeatureInfo.Title).CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);

    [AfterStep]
    public void AfterStep(ScenarioContext scenarioContext)
    {
        StepDefinitionType stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType;

        if (scenarioContext.ScenarioExecutionStatus.ToString().Equals("StepDefinitionPending"))
            _ = stepType switch
            {
                StepDefinitionType.Given => CurrentScenario
                    .CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text)
                    .Skip("Step Definition Pending"),
                StepDefinitionType.When => CurrentScenario
                    .CreateNode<When>(scenarioContext.StepContext.StepInfo.Text)
                    .Skip("Step Definition Pending"),
                StepDefinitionType.Then => CurrentScenario
                    .CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text)
                    .Skip("Step Definition Pending"),
                _ => throw new ArgumentOutOfRangeException(nameof(stepType), stepType, $"Invalid Step Type: {stepType}")
            };

        if (scenarioContext.TestError is null)
            _ = stepType switch
            {
                StepDefinitionType.Given => CurrentScenario
                    .CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text),
                StepDefinitionType.When => CurrentScenario
                    .CreateNode<When>(scenarioContext.StepContext.StepInfo.Text),
                StepDefinitionType.Then => CurrentScenario
                    .CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text),
                _ => throw new ArgumentOutOfRangeException(nameof(stepType), stepType, $"Invalid Step Type: {stepType}")
            };
    }

    [AfterTestRun]
    public static void AfterTestRun()
        => Extent.Flush();
}
