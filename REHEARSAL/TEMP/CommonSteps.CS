namespace REHEARSAL.TEMP;

[Binding]
internal sealed class CommonSteps : ProtoStep
{
    internal CommonSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext) { }

    [Given] [When] [Then]
    public void I_PASS_THE_STEP()
        => Assert.IsNull(null);

    [Given] [When] [Then]
    public void I_FAIL_THE_STEP()
        => Assert.IsNotNull(null);

    [Given] [When] [Then]
    public void I_RANDOMLY_EITHER_PASS_OR_FAIL_THE_STEP()
        => Assert.IsTrue(new[] {true, false}.RandomElement());
}
