namespace DRAMA.EventBindings;

[Binding]
public sealed class ExtentReportBindings // TODO: Remove This Type After Migrating Away From Extent Reports
{
    private static ExtentReports? Report { get; set; }
    private static List<ExtentTest>? Features { get; set; }
    private static List<ExtentTest>? Scenarios { get; set; }
    private static ExtentTest? CurrentFeature { get; set; }
    private static ExtentTest? CurrentScenario { get; set; }

    [BeforeTestRun(Order = 1)]
    public static void BeforeTestRun()
    {
        Profile profile = TestRunContext.Profile;

        ExtentHtmlReporter reporter = new(profile.TestRun?.ResultsPath, ViewStyle.SPA);

        reporter.Config.EnableTimeline = true;
        reporter.Config.ReportName = profile.TestRun?.Name;
        reporter.Config.DocumentTitle = profile.TestRun?.Name;
        reporter.Config.Theme = Theme.Dark;

        Report = new ExtentReports();
        Report.AttachReporter(reporter);

        Features = new List<ExtentTest>();
        Scenarios = new List<ExtentTest>();
    }

    [BeforeFeature(Order = 1)]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        CurrentFeature = Report?.CreateTest<Feature>(featureContext.FeatureInfo.Title) ?? throw new ConfigurationErrorsException("Extent Report Has Not Been Initialised");
        Features?.Add(CurrentFeature);
    }

    [BeforeScenario(Order = 1)]
    public void BeforeScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        CurrentScenario = CurrentFeature?.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title) ?? throw new ConfigurationErrorsException("Extent Feature Has Not Been Initialised");
        Scenarios?.Add(CurrentScenario);
    }

    [AfterStep(Order = 1)]
    public async Task AfterStep(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        if (featureContext.GetSkipFeatureSteps())
        {
            scenarioContext.StepContext.Status = ScenarioExecutionStatus.Skipped;
            return;
        }

        MediaEntityModelProvider? mediaEntity = featureContext.FeatureInfo.Tags.Intersect(new[] { "Front-End" }).Any() // TODO: Think Of A Better Trigger For Taking A Screenshot
            ? MediaEntityBuilder.CreateScreenCaptureFromBase64String
                (Convert.ToBase64String(await featureContext.GetCurrentBrowserTab().ScreenshotAsync(new PageScreenshotOptions { FullPage = true })), scenarioContext.ScenarioInfo.Title.Trim()).Build()
            : null;

        StepDefinitionType stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType;

        if (scenarioContext.StepContext.Status.Equals(ScenarioExecutionStatus.UndefinedStep))
            _ = stepType switch
            {
                StepDefinitionType.Given => CurrentScenario?.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Warning("Step Definition Is Undefined"),
                StepDefinitionType.When => CurrentScenario?.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Warning("Step Definition Is Undefined"),
                StepDefinitionType.Then => CurrentScenario?.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Warning("Step Definition Is Undefined"),
                _ => throw new NoMatchException($"Invalid Step Type: {stepType}")
            };

        if (scenarioContext.StepContext.Status.Equals(ScenarioExecutionStatus.StepDefinitionPending))
            _ = stepType switch
            {
                StepDefinitionType.Given => CurrentScenario?.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Info("Step Definition Is Pending"),
                StepDefinitionType.When => CurrentScenario?.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Info("Step Definition Is Pending"),
                StepDefinitionType.Then => CurrentScenario?.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Info("Step Definition Is Pending"),
                _ => throw new NoMatchException($"Invalid Step Type: {stepType}")
            };

        if (scenarioContext.StepContext.Status.Equals(ScenarioExecutionStatus.Skipped))
            _ = stepType switch
            {
                StepDefinitionType.Given => CurrentScenario?.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Skip("Step Has Been Skipped"),
                StepDefinitionType.When => CurrentScenario?.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Skip("Step Has Been Skipped"),
                StepDefinitionType.Then => CurrentScenario?.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Skip("Step Has Been Skipped"),
                _ => throw new NoMatchException($"Invalid Step Type: {stepType}")
            };

        if (scenarioContext.StepContext.Status.Equals(ScenarioExecutionStatus.OK))
            _ = stepType switch
            {
                StepDefinitionType.Given => CurrentScenario?.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Pass($"{DateTime.Now:R}", mediaEntity),
                StepDefinitionType.When => CurrentScenario?.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Pass($"{DateTime.Now:R}", mediaEntity),
                StepDefinitionType.Then => CurrentScenario?.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Pass($"{DateTime.Now:R}", mediaEntity),
                _ => throw new NoMatchException($"Invalid Step Type: {stepType}")
            };

        if (scenarioContext.StepContext.Status.Equals(ScenarioExecutionStatus.TestError))
            _ = stepType switch
            {
                StepDefinitionType.Given => CurrentScenario?.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message, mediaEntity),
                StepDefinitionType.When => CurrentScenario?.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message, mediaEntity),
                StepDefinitionType.Then => CurrentScenario?.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message, mediaEntity),
                _ => throw new NoMatchException($"Invalid Step Type: {stepType}")
            };

        if (scenarioContext.StepContext.Status.Equals(ScenarioExecutionStatus.BindingError))
            _ = stepType switch
            {
                StepDefinitionType.Given => CurrentScenario?.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Fatal(scenarioContext.TestError.StackTrace, mediaEntity),
                StepDefinitionType.When => CurrentScenario?.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Fatal(scenarioContext.TestError.StackTrace, mediaEntity),
                StepDefinitionType.Then => CurrentScenario?.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Fatal(scenarioContext.TestError.StackTrace, mediaEntity),
                _ => throw new NoMatchException($"Invalid Step Type: {stepType}")
            };
    }

    [AfterTestRun(Order = 1)]
    public static void AfterTestRun()
    {
        if (Scenarios is not null)
            foreach (ExtentTest scenario in Scenarios.Where(scenario => scenario.Model.HasChildren.Equals(false)))
                scenario.Skip("Scenario Has Been Skipped Due To An Error In A Previous Scenario");

        Report?.Flush();
    }
}
