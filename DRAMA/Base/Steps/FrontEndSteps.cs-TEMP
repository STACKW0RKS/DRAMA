namespace DRAMA.Base.Steps;

public abstract class FrontEndSteps : ProtoStepCollection
{
    protected IPage CurrentBrowserTab { get; set; }

    protected FrontEndSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext)
        => CurrentBrowserTab = FeatureContext.GetCurrentBrowserTab();

    protected FrontEndSteps(FeatureContext featureContext) : base(featureContext)
        => CurrentBrowserTab = FeatureContext.GetCurrentBrowserTab();
}
