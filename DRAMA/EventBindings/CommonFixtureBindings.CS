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

        if (scenarioContext.StepContext.Status.Equals(ScenarioExecutionStatus.OK).Equals(false))
            featureContext.SetErrorsHaveOccurred(true);
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
