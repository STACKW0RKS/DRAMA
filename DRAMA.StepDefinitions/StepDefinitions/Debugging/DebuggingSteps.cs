namespace DRAMA.StepDefinitions.StepDefinitions.Debugging;

[Binding]
internal sealed class DebuggingSteps : ProtoStepCollection
{
    internal DebuggingSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext) { }

    [Given, When, Then]
    public void I_IGNORE_THE_STEP()
        => Assert.Ignore("IGNORE");

    [Given, When, Then]
    public void I_SET_THE_STEP_INCONCLUSIVE()
        => Assert.Inconclusive("INCONCLUSIVE");

    [Given, When, Then]
    public void I_EXPLICITLY_PASS_THE_STEP()
        => Assert.Pass("PASS");

    [Given, When, Then]
    public void I_IMPLICITLY_PASS_THE_STEP()
        => Assert.Null(null, "NULL IS NULL");

    [Given, When, Then]
    public void I_RAISE_A_WARNING_FOR_THE_STEP()
        => Assert.Warn("WARN");

    [Given, When, Then]
    public void I_EXPLICITLY_FAIL_THE_STEP()
        => Assert.Fail("FAIL");

    [Given, When, Then]
    public void I_IMPLICITLY_FAIL_THE_STEP()
        => Assert.NotNull(null, "NULL IS NOT NULL");
}
