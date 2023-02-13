namespace DRAMA.EventBindings;

[Binding]
public class CommonFixtureBindings
{
    [BeforeTestRun(Order = 1)]
    public static void BeforeTestRun()
    {
        if (Contexts.TestRun.Profile.TestRun?.DebugLogging is true)
            Contexts.TestRun.PropertyBag?.Add<Stopwatch>("STOPWATCH", Stopwatch.StartNew());
    }

    [BeforeFeature(Order = 1)]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        if (Contexts.TestRun.Profile.TestRun?.DebugLogging is true)
            featureContext.Add<Stopwatch>(featureContext.FeatureInfo.Title, Stopwatch.StartNew());
    }

    [BeforeScenario(Order = 1)]
    public void BeforeScenario(ScenarioContext scenarioContext)
    {
        if (Contexts.TestRun.Profile.TestRun?.DebugLogging is true)
            scenarioContext.Add<Stopwatch>(scenarioContext.ScenarioInfo.Title, Stopwatch.StartNew());
    }

    [BeforeStep(Order = 1)]
    public void BeforeStep(IUnitTestRuntimeProvider unitTestRuntimeProvider, FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        if (Contexts.TestRun.Profile.TestRun?.DebugLogging is true)
            scenarioContext.Add<Stopwatch>(scenarioContext.StepContext.StepInfo.Text, Stopwatch.StartNew());

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
            Debug.WriteLine($@"[DEBUG] STEP     :: {scenarioContext.StepContext.StepInfo.Text}");
            Debug.WriteLine($@"[DEBUG] DURATION :: {scenarioContext.Get<Stopwatch>(scenarioContext.StepContext.StepInfo.Text).Elapsed}");

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
            Debug.WriteLine($@"[DEBUG] SCENARIO :: {scenarioContext.ScenarioInfo.Title}");
            Debug.WriteLine($@"[DEBUG] DURATION :: {scenarioContext.Get<Stopwatch>(scenarioContext.ScenarioInfo.Title).Elapsed}");

            scenarioContext.Remove<string>(scenarioContext.ScenarioInfo.Title);
        }
    }

    [AfterFeature(Order = 1)]
    public static void AfterFeature(FeatureContext featureContext)
    {
        if (Contexts.TestRun.Profile.TestRun?.DebugLogging is true)
        {
            Debug.WriteLine($@"[DEBUG] FEATURE  :: {featureContext.FeatureInfo.Title}");
            Debug.WriteLine($@"[DEBUG] DURATION :: {featureContext.Get<Stopwatch>(featureContext.FeatureInfo.Title).Elapsed}");
            featureContext.Remove<string>(featureContext.FeatureInfo.Title);
        }
    }

    [AfterTestRun(Order = 1)]
    public static void AfterTestRun()
    {
        if (Contexts.TestRun.Profile.TestRun?.DebugLogging is true)
        {
            Debug.WriteLine($@"[DEBUG] TEST RUN :: {Contexts.TestRun.Profile.TestRun.Name}");
            Debug.WriteLine($@"[DEBUG] DURATION :: {Contexts.TestRun.PropertyBag?.Get<Stopwatch>("STOPWATCH").Elapsed}");
        }
    }
}
