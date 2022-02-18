// TODO: Remove This Type After Migrating Away From Extent Reports

using DRAMA.Extensions.Specflow;

namespace DRAMA.EventBindings;

[Binding]
public class ExtentReportBindings
{
    private static ExtentReports? Report { get; set; }
    private static List<ExtentTest>? Features { get; set; }
    private static ExtentTest? CurrentScenario { get; set; }

    [BeforeTestRun(Order = 0)]
    public static void BeforeTestRun(TestThreadContext testThreadContext)
    {
        Profile profile = testThreadContext.GetTestRunProfile();

        ExtentHtmlReporter reporter = new(profile.TestRun?.ResultsPath, ViewStyle.SPA);

        reporter.Config.EnableTimeline = true;
        reporter.Config.ReportName = profile.TestRun?.Name;
        reporter.Config.DocumentTitle = profile.TestRun?.Name;
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

    [AfterStep(Order = 0)]
    public void AfterStep(ScenarioContext scenarioContext)
    {
        StepDefinitionType stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType;

        if (scenarioContext.ScenarioExecutionStatus.Equals(ScenarioExecutionStatus.StepDefinitionPending))
            _ = stepType switch
            {
                StepDefinitionType.Given => CurrentScenario?.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Skip("Step Definition Pending"),
                StepDefinitionType.When => CurrentScenario?.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Skip("Step Definition Pending"),
                StepDefinitionType.Then => CurrentScenario?.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Skip("Step Definition Pending"),
                _ => throw new ArgumentOutOfRangeException(nameof(stepType), stepType, $"Invalid Step Type: {stepType}")
            };

        if (scenarioContext.TestError is null)
            _ = stepType switch
            {
                StepDefinitionType.Given => CurrentScenario?.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text),
                StepDefinitionType.When => CurrentScenario?.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text),
                StepDefinitionType.Then => CurrentScenario?.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text),
                _ => throw new ArgumentOutOfRangeException(nameof(stepType), stepType, $"Invalid Step Type: {stepType}")
            };
    }

    [AfterStep("Front-End", "End-To-End", Order = 0)]
    public async Task AfterFrontEndStep(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        StepDefinitionType stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType;

        if (scenarioContext.TestError is not null)
        {
            MediaEntityModelProvider mediaEntity =
                    MediaEntityBuilder.CreateScreenCaptureFromBase64String
                        (Convert.ToBase64String(await featureContext.GetCurrentBrowserTab().ScreenshotAsync(new PageScreenshotOptions { FullPage = true })), scenarioContext.ScenarioInfo.Title.Trim()).Build();

            _ = stepType switch
            {
                StepDefinitionType.Given => CurrentScenario?.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message, mediaEntity),
                StepDefinitionType.When => CurrentScenario?.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message, mediaEntity),
                StepDefinitionType.Then => CurrentScenario?.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message, mediaEntity),
                _ => throw new ArgumentOutOfRangeException(nameof(stepType), stepType, $"Invalid Step Type: {stepType}")
            };
        }
    }

    [AfterStep("Back-End", Order = 0)]
    public void AfterBackEndStep(ScenarioContext scenarioContext)
    {
        StepDefinitionType stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType;

        if (scenarioContext.TestError is not null)
            _ = stepType switch
            {
                StepDefinitionType.Given => CurrentScenario?.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message),
                StepDefinitionType.When => CurrentScenario?.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message),
                StepDefinitionType.Then => CurrentScenario?.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message),
                _ => throw new ArgumentOutOfRangeException(nameof(stepType), stepType, $"Invalid Step Type: {stepType}")
            };
    }

    [AfterTestRun(Order = 0)]
    public static void AfterTestRun()
        => Report?.Flush();
}
