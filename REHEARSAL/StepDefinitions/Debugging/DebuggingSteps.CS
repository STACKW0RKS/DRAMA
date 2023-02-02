namespace REHEARSAL.StepDefinitions.Debugging;

[Binding]
internal sealed class DebuggingSteps : ProtoStep
{
    internal DebuggingSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext) { }

    [Given, When, Then]
    public void I_IGNORE_THE_STEP()
        => Assert.Ignore("IGNORE");

    [Given, When, Then]
    public void I_SET_THE_STEP_INCONCLUSIVE()
        => Assert.Inconclusive("INCONCLUSIVE");

    [Given, When, Then]
    public void I_PASS_THE_STEP()
        => Assert.Pass("PASS");

    [Given, When, Then]
    public void I_RAISE_A_WARNING_FOR_THE_STEP()
        => Assert.Warn("WARN");

    [Given, When, Then]
    public void I_FAIL_THE_STEP()
        => Assert.Fail("FAIL");

    [When(@"I READ THE ""(.*)"" PROPERTY")]
    public void I_READ_THE_P1_PROPERTY(string property)
        => Assert.NotNull(TestRun.PropertyBag?[$"{property}"]);

    [When(@"I STORE THE ""(.*)"" PROPERTY WITH THE VALUE ""(.*)""")]
    public void I_STORE_THE_P1_PROPERTY_WITH_THE_VALUE_P2(string property, string value)
        => TestRun.PropertyBag?.Add(property, value);

    [Then(@"THE ""(.*)"" PROPERTY SHOULD BE ""(.*)""")]
    public void THE_P1_PROPERTY_SHOULD_BE_P2(string property, string value)
        => Assert.AreEqual(value, TestRun.PropertyBag?[$"{property}"]);
}
