namespace DRAMA.StepDefinitions.StepDefinitions.FrontEnd;

[Binding]
internal sealed class FrontEndSteps : ProtoStep
{
    internal FrontEndSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext) { }

    [When(@"I NAVIGATE TO ""(.*)""")]
    public async Task I_NAVIGATE_TO_STRING(string URI)
        => await FeatureContext.GetCurrentBrowserTab().GotoAsync(URI);

    [Then(@"I CONFIRM THAT THE PAGE TITLE CONTAINS ""(.*)""")]
    public async Task I_CONFIRM_THAT_THE_PAGE_TITLE_CONTAINS_STRING(string title)
        => Assert.IsTrue((await FeatureContext.GetCurrentBrowserTab().TitleAsync()).Contains(title), "Page Title");

    [Then]
    public async Task I_TAKE_A_SCREENSHOT()
        => await FeatureContext.GetCurrentBrowserTab()
            .ScreenshotAsync(new PageScreenshotOptions { FullPage = true, Path = "screenshots" + Path.DirectorySeparatorChar + $"{DateTime.Now:yyyyMMddHHmmss}.png" });
}
