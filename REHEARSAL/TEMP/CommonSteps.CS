namespace REHEARSAL.TEMP;

[Binding]
internal sealed class CommonSteps : ProtoStep
{
    internal CommonSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext) { }

    [When]
    public void I_PASS_THE_STEP()
        => Assert.IsNull(null);
}
