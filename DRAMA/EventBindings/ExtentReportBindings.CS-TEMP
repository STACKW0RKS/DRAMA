namespace DRAMA.EventBindings;

public static class ExtentTestExtensions
{
    public static void SetFeatureIdentifier(this ExtentTest test, Guid identifier)
        => test.Model.Description = identifier.ToString();

    public static Guid GetFeatureIdentifier(this ExtentTest context)
        => new Guid(context.Model.Description);
}

[Binding]
public sealed class ExtentReportBindings // TODO: Remove This Type After Migrating Away From Extent Reports (A Good Alternative Would Be Allure Framework)
{
    private static ExtentReports Report { get; set; } = new();
    private static Dictionary<ExtentTest, List<ExtentTest>> Features { get; set; } = new();

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        Profile profile = Contexts.TestRun.Profile;

        ExtentHtmlReporter reporter = new(profile.TestRun?.ResultsPath, ViewStyle.SPA);

        reporter.Config.EnableTimeline = true;
        reporter.Config.ReportName = profile.TestRun?.Name;
        reporter.Config.DocumentTitle = profile.TestRun?.Name;
        reporter.Config.Theme = Theme.Dark;

        Report.AttachReporter(reporter);
    }

    [BeforeFeature]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        ExtentTest feature = Report.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        feature.SetFeatureIdentifier(featureContext.GetIdentifier());

        Features.Add(feature, new List<ExtentTest>());
    }

    [BeforeScenario]
    public void BeforeScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        ExtentTest feature = Features.Single(feature => feature.Key.GetFeatureIdentifier() == featureContext.GetIdentifier()).Key;

        ExtentTest scenario = feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        scenario.SetFeatureIdentifier(scenarioContext.GetFeatureIdentifier());

        Features[feature].Add(scenario);
    }

    [AfterStep]
    public async Task AfterStep(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        if (featureContext.GetSkipFeatureSteps())
        {
            scenarioContext.StepContext.Status = ScenarioExecutionStatus.Skipped;
        }

        MediaEntityModelProvider? mediaEntity = featureContext.FeatureInfo.Tags.Intersect(new[] { "Front-End" }).Any() // TODO: Think Of A Better Trigger For Taking A Screenshot
            ? MediaEntityBuilder.CreateScreenCaptureFromBase64String
                (Convert.ToBase64String(await featureContext.GetCurrentBrowserTab().ScreenshotAsync(new PageScreenshotOptions { FullPage = true })), scenarioContext.ScenarioInfo.Title.Trim()).Build()
            : null;

        StepDefinitionType stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType;

        ExtentTest scenario = Features.Single(feature => feature.Key.GetFeatureIdentifier() == featureContext.GetIdentifier()).Value
            .Last(scenario => scenario.GetFeatureIdentifier() == scenarioContext.GetFeatureIdentifier());

        if (scenarioContext.StepContext.Status.Equals(ScenarioExecutionStatus.UndefinedStep))
            _ = stepType switch
            {
                StepDefinitionType.Given => scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Warning("Step Definition Is Undefined"),
                StepDefinitionType.When => scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Warning("Step Definition Is Undefined"),
                StepDefinitionType.Then => scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Warning("Step Definition Is Undefined"),
                _ => throw new NoMatchException($"Invalid Step Type: {stepType}")
            };

        if (scenarioContext.StepContext.Status.Equals(ScenarioExecutionStatus.StepDefinitionPending))
            _ = stepType switch
            {
                StepDefinitionType.Given => scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Info("Step Definition Is Pending"),
                StepDefinitionType.When => scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Info("Step Definition Is Pending"),
                StepDefinitionType.Then => scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Info("Step Definition Is Pending"),
                _ => throw new NoMatchException($"Invalid Step Type: {stepType}")
            };

        if (scenarioContext.StepContext.Status.Equals(ScenarioExecutionStatus.Skipped))
            _ = stepType switch
            {
                StepDefinitionType.Given => scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Skip("Step Has Been Skipped"),
                StepDefinitionType.When => scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Skip("Step Has Been Skipped"),
                StepDefinitionType.Then => scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Skip("Step Has Been Skipped"),
                _ => throw new NoMatchException($"Invalid Step Type: {stepType}")
            };

        if (scenarioContext.StepContext.Status.Equals(ScenarioExecutionStatus.OK))
            _ = stepType switch
            {
                StepDefinitionType.Given => scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Pass($"{DateTime.Now:R}", mediaEntity),
                StepDefinitionType.When => scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Pass($"{DateTime.Now:R}", mediaEntity),
                StepDefinitionType.Then => scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Pass($"{DateTime.Now:R}", mediaEntity),
                _ => throw new NoMatchException($"Invalid Step Type: {stepType}")
            };

        if (scenarioContext.StepContext.Status.Equals(ScenarioExecutionStatus.TestError))
            _ = stepType switch
            {
                StepDefinitionType.Given => scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message, mediaEntity),
                StepDefinitionType.When => scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message, mediaEntity),
                StepDefinitionType.Then => scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message, mediaEntity),
                _ => throw new NoMatchException($"Invalid Step Type: {stepType}")
            };

        if (scenarioContext.StepContext.Status.Equals(ScenarioExecutionStatus.BindingError))
            _ = stepType switch
            {
                StepDefinitionType.Given => scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Fatal(scenarioContext.TestError.StackTrace, mediaEntity),
                StepDefinitionType.When => scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Fatal(scenarioContext.TestError.StackTrace, mediaEntity),
                StepDefinitionType.Then => scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Fatal(scenarioContext.TestError.StackTrace, mediaEntity),
                _ => throw new NoMatchException($"Invalid Step Type: {stepType}")
            };
    }

    [AfterFeature]
    public static void AfterFeature(FeatureContext featureContext)
    {
        ExtentTest feature = Features.Single(feature => feature.Key.GetFeatureIdentifier() == featureContext.GetIdentifier()).Key;
        List<ExtentTest> scenarios = Features[feature];

        if (scenarios.Any())
            foreach (ExtentTest scenario in scenarios.Where(scenario => scenario.Model.HasChildren.Equals(false)))
                scenario.Skip("Scenario Has Been Skipped Due To An Error In A Previous Scenario");
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        Report.Flush();
    }
}
