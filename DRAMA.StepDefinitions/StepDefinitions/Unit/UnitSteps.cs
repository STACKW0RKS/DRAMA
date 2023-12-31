namespace DRAMA.StepDefinitions.StepDefinitions.Unit;

[Binding]
internal sealed class UnitSteps : ProtoStepCollection
{
    internal UnitSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext) { }

    [When(@"I READ THE ""(.*)"" PROPERTY")]
    public void I_READ_THE_P1_PROPERTY(string key)
        => Assert.NotNull(TestRunContext.PropertyBag?.Get<object>(key));

    [When(@"I STORE THE ""(.*)"" PROPERTY WITH THE VALUE ""(.*)""")]
    public void I_STORE_THE_P1_PROPERTY_WITH_THE_VALUE_P2(string key, string value)
        => TestRunContext.PropertyBag?.Set<string>(key, value);

    [Then(@"THE ""(.*)"" PROPERTY SHOULD BE ""(.*)""")]
    public void THE_P1_PROPERTY_SHOULD_BE_P2(string key, string value)
        => Assert.AreEqual(value, TestRunContext.PropertyBag?.Get<string>(key));

    [When(@"I STORE THE STRING VALUE ""(.*)"" INTO ENVIRONMENT VARIABLE ""(.*)""")]
    public static void I_STORE_THE_STRING_VALUE_P0_INTO_ENVIRONMENT_VARIABLE_P1(string value, string key)
        => Environment.SetEnvironmentVariable(key, value);

    [When(@"I STORE THE INTEGER VALUE ""(.*)"" INTO ENVIRONMENT VARIABLE ""(.*)""")]
    public static void I_STORE_THE_INTEGER_VALUE_P0_INTO_ENVIRONMENT_VARIABLE_P1(int value, string key)
        => Environment.SetEnvironmentVariable(key, value.ToString());

    [When(@"I STORE THE STRING VALUE ""(.*)"" WITH KEY ""(.*)"" INTO THE TEST RUN CONTEXT")]
    public static void I_STORE_THE_STRING_VALUE_P0_WITH_KEY_P1_INTO_THE_TEST_RUN_CONTEXT(string value, string key)
        => TestRunContext.PropertyBag?.Add(key, value);

    [When(@"I STORE THE INTEGER VALUE ""(.*)"" WITH KEY ""(.*)"" INTO THE TEST RUN CONTEXT")]
    public static void I_STORE_THE_INTEGER_VALUE_P0_WITH_KEY_P1_INTO_THE_TEST_RUN_CONTEXT(int value, string key)
        => TestRunContext.PropertyBag?.Add(key, value);

    [When(@"I STORE THE STRING VALUE ""(.*)"" WITH KEY ""(.*)"" INTO THE FEATURE CONTEXT")]
    public void I_STORE_THE_STRING_VALUE_P0_WITH_KEY_P1_INTO_THE_FEATURE_CONTEXT(string value, string key)
        => FeatureContext.Set(key: key, value: value);

    [When(@"I STORE THE INTEGER VALUE ""(.*)"" WITH KEY ""(.*)"" INTO THE FEATURE CONTEXT")]
    public void I_STORE_THE_INTEGER_VALUE_P0_WITH_KEY_P1_INTO_THE_FEATURE_CONTEXT(int value, string key)
        => FeatureContext.Set(key: key, value: value);

    [When(@"I STORE THE STRING VALUE ""(.*)"" WITH KEY ""(.*)"" INTO THE SCENARIO CONTEXT")]
    public void I_STORE_THE_STRING_VALUE_P0_WITH_KEY_P1_INTO_THE_SCENARIO_CONTEXT(string value, string key)
        => ScenarioContext.Set(key: key, value: value);

    [When(@"I STORE THE INTEGER VALUE ""(.*)"" WITH KEY ""(.*)"" INTO THE SCENARIO CONTEXT")]
    public void I_STORE_THE_INTEGER_VALUE_P0_WITH_KEY_P1_INTO_THE_SCENARIO_CONTEXT(int value, string key)
        => ScenarioContext.Set(key: key, value: value);

    [Then(@"TOKENISED VALUE ""(.*)"" SHOULD EQUAL DETOKENISED VALUE ""(.*)""")]
    public void TOKENISED_VALUE_P0_SHOULD_EQUAL_DETOKENISED_VALUE_P1(string key, string value)
        => Assert.That(TokenHelpers.Detokenise(key, FeatureContext, ScenarioContext), Is.EqualTo(value));
}
