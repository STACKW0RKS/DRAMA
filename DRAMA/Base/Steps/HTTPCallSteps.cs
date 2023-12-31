namespace DRAMA.Base.Steps;

public abstract class HTTPCallSteps : ProtoStepCollection
{
    protected IAPIRequestContext HTTPCallContext => FeatureContext.GetCurrentHTTPCallContext();

    protected string Token => FeatureContext.GetAPIAuthenticationToken();

    protected HTTPCallSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext) { }

    protected HTTPCallSteps(FeatureContext featureContext) : base(featureContext) { }

    protected static string GetURI(string path)
        => URIHelpers.ResolveEndpointURI(path).AbsoluteUri;

    protected static string GetURI(string host, string path)
        => URIHelpers.ResolveEndpointURI(TestRunContext.Profile.TestRun?.PropertyBag?.Get<string>(host) ?? "localhost", path).AbsoluteUri;
}
