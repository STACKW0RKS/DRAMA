// TODO: Remove This After Migrating Away From Extent Reports

using System.Configuration;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using DRAMA.Contexts;
using TechTalk.SpecFlow.Bindings;

namespace DRAMA.EventBindings;

[Binding]
public class ExtentReportBindings
{
    private static ExtentReports? Report { get; set; }
    private static List<ExtentTest>? Features { get; set; }
    private static ExtentTest? CurrentScenario { get; set; }

    [BeforeTestRun(Order = 0)]
    public static void BeforeTestRun()
    {
        ExtentHtmlReporter reporter = new(TestRunContext.Profile?.TestRun?.ResultsPath, ViewStyle.SPA);

        reporter.Config.EnableTimeline = true;
        reporter.Config.ReportName = TestRunContext.Profile?.TestRun?.Name;
        reporter.Config.DocumentTitle = TestRunContext.Profile?.TestRun?.Name;
        reporter.Config.Theme = Theme.Dark;

        Report = new ExtentReports();
        Report.AttachReporter(reporter);

        Features = new List<ExtentTest>();
    }

    [BeforeFeature(Order = 0)]
    public static void BeforeFeature(FeatureContext featureContext)
        => Features?.Add(Report?.CreateTest<Feature>(featureContext.FeatureInfo.Title) ?? throw new ConfigurationErrorsException("Extent Report Has Not Been Initialised"));

    [BeforeScenario(Order = 0)]
    public void BeforeScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        => CurrentScenario = Features?.Single(feature => feature.Model.Name == featureContext.FeatureInfo.Title).CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);

    [BeforeStep(Order = 0)]
    public void BeforeStep() { }

    [AfterStep(Order = 0)]
    public void AfterStep(ScenarioContext scenarioContext)
    {
        StepDefinitionType stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType;

        if (scenarioContext.ScenarioExecutionStatus.ToString().Equals("StepDefinitionPending"))
            _ = stepType switch
            {
                StepDefinitionType.Given => CurrentScenario?
                    .CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text)
                    .Skip("Step Definition Pending"),
                StepDefinitionType.When => CurrentScenario?
                    .CreateNode<When>(scenarioContext.StepContext.StepInfo.Text)
                    .Skip("Step Definition Pending"),
                StepDefinitionType.Then => CurrentScenario?
                    .CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text)
                    .Skip("Step Definition Pending"),
                _ => throw new ArgumentOutOfRangeException(nameof(stepType), stepType, $"Invalid Step Type: {stepType}")
            };

        if (scenarioContext.TestError is null)
            _ = stepType switch
            {
                StepDefinitionType.Given => CurrentScenario?
                    .CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text),
                StepDefinitionType.When => CurrentScenario?
                    .CreateNode<When>(scenarioContext.StepContext.StepInfo.Text),
                StepDefinitionType.Then => CurrentScenario?
                    .CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text),
                _ => throw new ArgumentOutOfRangeException(nameof(stepType), stepType, $"Invalid Step Type: {stepType}")
            };
    }

    [AfterScenario(Order = 0)]
    public void AfterScenario() { }

    [AfterFeature(Order = 0)]
    public static void AfterFeature() { }

    [AfterTestRun(Order = 0)]
    public static void AfterTestRun()
        => Report?.Flush();
}
