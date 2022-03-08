namespace DRAMA.EventBindings;

[Binding]
public class CommonFixtureBindings
{
    [BeforeStep(Order = 1)]
    public void BeforeStep(IUnitTestRuntimeProvider unitTestRuntimeProvider, FeatureContext featureContext)
    {
        if ((TestRunContext.Profile.TestRun?.StopFeatureAtFirstError ?? false) && featureContext.GetErrorsHaveOccurred().Equals(true))
        {
            featureContext.SetSkipFeatureSteps(true);
            unitTestRuntimeProvider.TestIgnore("Step Has Been Skipped Due To An Error In A Previous Step");
        }
    }

    [AfterStep(Order = 1)]
    public void AfterStep(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        if (scenarioContext.StepContext.Status.Equals(ScenarioExecutionStatus.OK).Equals(false))
            featureContext.SetErrorsHaveOccurred(true);
    }
}
