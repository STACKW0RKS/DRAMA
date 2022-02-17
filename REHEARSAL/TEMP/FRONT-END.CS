namespace REHEARSAL.TEMP;

[Binding]
internal sealed class FrontEndSteps : ProtoStep
{
    internal FrontEndSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext) { }

    [When(@"I NAVIGATE TO ""(.*)""")]
    public async Task I_NAVIGATE_TO_STRING(string URI)
    {
        await Task.Delay(5);
    }

    [Then(@"I CONFIRM THAT THE PAGE TITLE IS ""(.*)""")]
    public async Task I_CONFIRM_THAT_THE_PAGE_TITLE_IS_STRING(string title)
    {
        await Task.Delay(5);
    }

    [Then(@"I TAKE A SCREENSHOT")]
    public async Task I_TAKE_A_SCREENSHOT()
    {
        await Task.Delay(5);
    }
}
