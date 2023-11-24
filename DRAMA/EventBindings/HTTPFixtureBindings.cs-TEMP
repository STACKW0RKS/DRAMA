namespace DRAMA.EventBindings;

[Binding]
public sealed class HTTPFixtureBindings
{
    [BeforeFeature("API", Order = 2)]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        if (Contexts.TestRun.Profile.TestRun?.DebugLogging is true)
            LogHelpers.Log($@"[DEBUG] [FEATURE___] INFO :: Current Playwright Version Is v{Assembly.GetAssembly(typeof(Playwright))?.GetName().Version}");
    }

    [BeforeStep("API", Order = 2)]
    public async Task BeforeStep(FeatureContext featureContext)
    {
        await EventBindingHelpers.InitialiseHTTPCallContext(featureContext);
    }

    [AfterStep("API", Order = 2)]
    public async Task AfterStep(FeatureContext featureContext)
    {
        IAPIRequestContext webCallContext = featureContext.GetCurrentHTTPCallContext();

        await webCallContext.DisposeAsync();
    }
}
