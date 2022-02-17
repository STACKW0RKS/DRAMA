using DRAMA.Contexts;
using DRAMA.Helpers.Configuration;

namespace DRAMA.EventBindings;

[Binding]
public class CommonFixtureBindings
{
    public CommonFixtureBindings(FeatureContext featureContext, ScenarioContext scenarioContext) { }

    [BeforeTestRun(Order = 0)]
    public static async Task BeforeTestRun()
        => TestRunContext.Profile = await ConfigurationParser.GetTestExecutionProfile();
}
