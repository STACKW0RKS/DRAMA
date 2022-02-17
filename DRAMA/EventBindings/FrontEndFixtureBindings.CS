namespace DRAMA.EventBindings;

[Binding]
public class FrontEndFixtureBindings
{
    [BeforeFeature("Front-End", "End-To-End", Order = 0)]
    public static async Task BeforeUIFeature(FeatureContext featureContext)
    {
        PlaywrightDriver playwrightDriver = new();
        (IBrowser Browser, IPage Page) = await playwrightDriver.InitialisePlaywrightDriver(Z34.TestRunBrowser, Z34.TestRunLaunchOptions);
        featureContext.SetBrowserInstance(Browser);
        featureContext.SetPageInstance(Page);
    }

    [AfterFeature("Front-End", "End-To-End", Order = 0)]
    public static async Task AfterUIFeature(FeatureContext featureContext)
        => await featureContext.GetBrowserInstance().Contexts.Single().Tracing.StopAsync(new TracingStopOptions
            { Path = Path.Combine(Z34.TestResultsDirectory, $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.zip") });
}
