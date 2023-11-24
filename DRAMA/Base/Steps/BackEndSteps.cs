namespace DRAMA.Base.Steps;

public abstract class BackEndSteps : ProtoStepCollection
{
    protected IDbConnection CurrentDatabaseConnection { get; set; }

    protected BackEndSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext)
        => CurrentDatabaseConnection = FeatureContext.GetDatabaseConnection();

    protected BackEndSteps(FeatureContext featureContext) : base(featureContext)
        => CurrentDatabaseConnection = FeatureContext.GetDatabaseConnection();
}
