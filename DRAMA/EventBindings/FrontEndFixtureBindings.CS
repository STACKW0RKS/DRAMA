using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using TechTalk.SpecFlow.Bindings;

namespace DRAMA.EventBindings;

[Binding]
public class FrontEndFixtureBindings
{
    private ExtentTest CurrentScenario;

    [BeforeFeature("Front-End", "End-To-End", Order = 0)]
    public static async Task BeforeUIFeature(FeatureContext featureContext)
    {
        PlaywrightDriver playwrightDriver = new();
        (IBrowser Browser, IPage Page) = await playwrightDriver.InitialisePlaywrightDriver(Z34.TestRunBrowser, Z34.TestRunLaunchOptions);
        featureContext.SetBrowserInstance(Browser);
        featureContext.SetPageInstance(Page);
    }

    [AfterStep("Front-End", "End-To-End", Order = 0)]
    public async Task AfterUIStep(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        StepDefinitionType stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType;

        if (scenarioContext.TestError is not null)
        {
            MediaEntityModelProvider mediaEntity = await featureContext.GetPageInstance()
                .CaptureScreenshotAndReturnModel(scenarioContext.ScenarioInfo.Title.Trim());

            _ = stepType switch
            {
                StepDefinitionType.Given => CurrentScenario
                    .CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text)
                    .Fail(scenarioContext.TestError.Message, mediaEntity),
                StepDefinitionType.When => CurrentScenario
                    .CreateNode<When>(scenarioContext.StepContext.StepInfo.Text)
                    .Fail(scenarioContext.TestError.Message, mediaEntity),
                StepDefinitionType.Then => CurrentScenario
                    .CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text)
                    .Fail(scenarioContext.TestError.Message, mediaEntity),
                _ => throw new ArgumentOutOfRangeException(nameof(stepType), stepType, $"Invalid Step Type: {stepType}")
            };
        }
    }

    [AfterFeature("Front-End", "End-To-End", Order = 0)]
    public static async Task AfterUIFeature(FeatureContext featureContext)
        => await featureContext.GetBrowserInstance().Contexts.Single().Tracing.StopAsync(new TracingStopOptions
        { Path = Path.Combine(Z34.TestResultsDirectory, $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.zip") });
}
