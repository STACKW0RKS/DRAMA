namespace DRAMA.EventBindings;

[Binding]
public class CommonFixtureBindings
{
    [BeforeStep(Order = 1)]
    public void BeforeStep(FeatureContext featureContext)
    {
        ScenarioStepContext asd = ScenarioStepContext.Current;

        if ((TestRunContext.Profile.TestRun?.StopFeatureAtFirstError ?? false) && featureContext.GetErrorsHaveOccurred().Equals(true))
            //scenarioContext.StepContext.Status = ScenarioExecutionStatus.Skipped;
            asd.Status = ScenarioExecutionStatus.Skipped;
    }

    [AfterStep(Order = 1)]
    public void AfterStep(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        if (scenarioContext.StepContext.Status.Equals(ScenarioExecutionStatus.OK).Equals(false))
            featureContext.SetErrorsHaveOccurred(true);
    }
}
