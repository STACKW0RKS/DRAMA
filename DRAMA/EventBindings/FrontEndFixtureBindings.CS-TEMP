namespace DRAMA.EventBindings;

[Binding]
public sealed class FrontEndFixtureBindings
{
    [BeforeFeature("Front-End", Order = 2)]
    public static async Task BeforeFeature(FeatureContext featureContext)
    {
        await EventBindingHelpers.InitialiseBrowserTab(featureContext);
    }

    [AfterFeature("Front-End", Order = 2)]
    public static async Task AfterFeature(FeatureContext featureContext)
    {
        IBrowserContext browserContext = featureContext.Get<IBrowser>("Browser").Contexts.Single();

        await browserContext.Tracing.StopAsync(new TracingStopOptions
        { Path = Path.Combine(Contexts.TestRun.Profile.TestRun?.ResultsPath ?? string.Empty, featureContext.FeatureInfo.Title.ToPathCompatible(), $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.zip") });

        // Required for the generation of HAR files. Closing The browser context triggers the HAR files to flush.
        await browserContext.CloseAsync();
    }
}
