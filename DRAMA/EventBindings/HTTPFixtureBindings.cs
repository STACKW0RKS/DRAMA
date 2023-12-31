namespace DRAMA.EventBindings;

[Binding]
public sealed class HTTPFixtureBindings
{
    [BeforeFeature(["API(IAPIRequestContext)", "API"], Order = 2)]
    public static void BeforeFeatureIAPIRequestContext(FeatureContext featureContext)
    {
        if (TestRunContext.Profile.TestRun?.DebugLogging is true)
            LogHelpers.Log($@"[DEBUG] [FEATURE___] INFO :: Current Playwright Version Is v{Assembly.GetAssembly(typeof(Playwright))?.GetName().Version}");
    }

    [BeforeStep(["API(IAPIRequestContext)", "API"], Order = 2)]
    public async Task BeforeStepIAPIRequestContext(FeatureContext featureContext)
    {
        await EventBindingHelpers.InitialiseHTTPCallContext(featureContext);
    }

    [BeforeStep("API(HttpClient)", Order = 2)]
    public void BeforeStepHttpClient(FeatureContext featureContext)
    {
        EventBindingHelpers.InitialiseHTTPClient(featureContext);
    }

    [AfterStep(["API(IAPIRequestContext)", "API"], Order = 2)]
    public async Task AfterStepIAPIRequestContext(FeatureContext featureContext)
    {
        try
        {
            IAPIRequestContext webCallContext = featureContext.GetCurrentHTTPCallContext();

            await webCallContext.DisposeAsync();
        }

        catch (Exception exception)
        {
            // Disposing the web call context after a test step error will throw the following exception: "Microsoft.Playwright.TargetClosedException : Target page, context or browser has been closed.".

            if (exception.Source is not "Microsoft.Playwright")
                LogHelpers.Log($@"[DEBUG] [TEST_STEP_] INFO :: {exception.Message}.");
        }
    }

    [AfterStep("API(HttpClient)", Order = 2)]
    public void AfterStepHttpClient(FeatureContext featureContext)
    {
        featureContext.GetCurrentHTTPClient()?.Dispose();
    }
}
