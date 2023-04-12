namespace DRAMA.EventBindings;

[Binding]
public class CommonFixtureBindings
{
    [BeforeTestRun(Order = 1)]
    public static void BeforeTestRun()
    {
        if (Contexts.TestRun.Profile.TestRun?.DebugLogging is true)
        {
            Contexts.TestRun.PropertyBag!.Add<Stopwatch>("STOPWATCH", Stopwatch.StartNew());

            LogHelpers.Log($@"[DEBUG] [TEST_RUN__] NAME :: {Contexts.TestRun.Profile.TestRun.Name}");
            LogHelpers.Log($@"[DEBUG] [TEST_RUN__] INIT :: {DateTimeOffset.Now:HH:mm:ss.fff}");
        }
    }

    [BeforeFeature(Order = 1)]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        featureContext.SetIdentifier(Guid.NewGuid());

        if (Contexts.TestRun.Profile.TestRun?.DebugLogging is true)
        {
            featureContext.Add<Stopwatch>(featureContext.FeatureInfo.Title, Stopwatch.StartNew());

            LogHelpers.Log($@"[DEBUG] [FEATURE___] NAME :: {featureContext.FeatureInfo.Title}");
            LogHelpers.Log($@"[DEBUG] [FEATURE___] INIT :: {DateTimeOffset.Now:HH:mm:ss.fff}");
        }
    }

    [BeforeScenario(Order = 1)]
    public void BeforeScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        scenarioContext.SetFeatureIdentifier(featureContext.GetIdentifier());

        if (Contexts.TestRun.Profile.TestRun?.DebugLogging is true)
        {
            scenarioContext.Add<Stopwatch>(scenarioContext.ScenarioInfo.Title, Stopwatch.StartNew());

            LogHelpers.Log($@"[DEBUG] [SCENARIO__] NAME :: {scenarioContext.ScenarioInfo.Title}");
            LogHelpers.Log($@"[DEBUG] [SCENARIO__] INIT :: {DateTimeOffset.Now:HH:mm:ss.fff}");
        }
    }

    [BeforeStep(Order = 1)]
    public void BeforeStep(IUnitTestRuntimeProvider unitTestRuntimeProvider, FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        if (Contexts.TestRun.Profile.TestRun?.DebugLogging is true)
        {
            scenarioContext.Add<Stopwatch>(scenarioContext.StepContext.StepInfo.Text, Stopwatch.StartNew());

            LogHelpers.Log($@"[DEBUG] [TEST_STEP_] NAME :: {scenarioContext.StepContext.StepInfo.Text}");
            LogHelpers.Log($@"[DEBUG] [TEST_STEP_] INIT :: {DateTimeOffset.Now:HH:mm:ss.fff}");
        }

        if ((Contexts.TestRun.Profile.TestRun?.StopFeatureAtFirstError ?? false) && featureContext.GetErrorsHaveOccurred().Equals(true))
        {
            featureContext.SetSkipFeatureSteps(true);
            unitTestRuntimeProvider.TestIgnore("Step Has Been Skipped Due To An Error In A Previous Step");
        }
    }

    [AfterStep(Order = 1)]
    public void AfterStep(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        if (Contexts.TestRun.Profile.TestRun?.DebugLogging is true)
        {
            TimeSpan elapsed = scenarioContext.Get<Stopwatch>(scenarioContext.StepContext.StepInfo.Text).Elapsed;

            LogHelpers.Log($@"[DEBUG] [TEST_STEP_] NAME :: {scenarioContext.StepContext.StepInfo.Text}");
            LogHelpers.Log($@"[DEBUG] [TEST_STEP_] DONE :: {elapsed.Hours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}.{elapsed.Milliseconds:D3}");

            scenarioContext.Remove<string>(scenarioContext.StepContext.StepInfo.Text);
        }

        // If the ScenarioExecutionStatus (for the current step) is not OK, then get the value of TestError.ResultState.
        // When ScenarioExecutionStatus is not OK, TestError should never be NULL.
        // ResultState seems to be injected at runtime, so it needs to be retrieved via Reflection.

        bool failure = scenarioContext.ScenarioExecutionStatus is not ScenarioExecutionStatus.OK &&
                       (scenarioContext.TestError.GetType().GetProperty("ResultState")?.GetValue(scenarioContext.TestError) is not ResultState result ||
                       result.Status switch
                       {
                           TestStatus.Skipped => false,
                           TestStatus.Inconclusive => false,
                           TestStatus.Passed => false,
                           TestStatus.Warning => false,
                           TestStatus.Failed => true,
                           _ => throw new ArgumentOutOfRangeException($@"Unknown Test Status: {result.Status}")
                       });

        // TODO: TestError.ResultState is specific to NUnit, so the code above only works with NUnit. Make the code above also work with MSTest and XUnit.
        // LINK: https://docs.specflow.org/projects/specflow/en/latest/Installation/Unit-Test-Providers.html

        if (failure) featureContext.SetErrorsHaveOccurred(true);
    }

    [AfterScenario(Order = 1)]
    public void AfterScenario(ScenarioContext scenarioContext)
    {
        if (Contexts.TestRun.Profile.TestRun?.DebugLogging is true)
        {
            TimeSpan elapsed = scenarioContext.Get<Stopwatch>(scenarioContext.ScenarioInfo.Title).Elapsed;

            LogHelpers.Log($@"[DEBUG] [SCENARIO__] NAME :: {scenarioContext.ScenarioInfo.Title}");
            LogHelpers.Log($@"[DEBUG] [SCENARIO__] DONE :: {elapsed.Hours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}.{elapsed.Milliseconds:D3}");

            scenarioContext.Remove<string>(scenarioContext.ScenarioInfo.Title);
        }
    }

    [AfterFeature(Order = 1)]
    public static void AfterFeature(FeatureContext featureContext)
    {
        if (Contexts.TestRun.Profile.TestRun?.DebugLogging is true)
        {
            TimeSpan elapsed = featureContext.Get<Stopwatch>(featureContext.FeatureInfo.Title).Elapsed;

            LogHelpers.Log($@"[DEBUG] [FEATURE___] NAME :: {featureContext.FeatureInfo.Title}");
            LogHelpers.Log($@"[DEBUG] [FEATURE___] DONE :: {elapsed.Hours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}.{elapsed.Milliseconds:D3}");

            featureContext.Remove<string>(featureContext.FeatureInfo.Title);
        }
    }

    [AfterTestRun(Order = 1)]
    public static void AfterTestRun()
    {
        if (Contexts.TestRun.Profile.TestRun?.DebugLogging is true)
        {
            TimeSpan elapsed = Contexts.TestRun.PropertyBag!.Get<Stopwatch>("STOPWATCH").Elapsed;

            LogHelpers.Log($@"[DEBUG] [TEST_RUN__] NAME :: {Contexts.TestRun.Profile.TestRun.Name}");
            LogHelpers.Log($@"[DEBUG] [TEST_RUN__] DONE :: {elapsed.Hours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}.{elapsed.Milliseconds:D3}");

            Contexts.TestRun.PropertyBag?.Remove<Stopwatch>("STOPWATCH");
        }
    }
}
