namespace DRAMA.Base.Steps;

/// <summary>
///     Make HTTP requests and receive HTTP responses using IAPIRequestContext (<a href="https://playwright.dev/dotnet/docs/api/class-apirequestcontext"></a>).
///     <br/>
///     Does not support in-memory calls, for purposes such as integration testing using a WebApplicationFactory.
/// </summary>
public abstract class HTTPCallContextSteps : BaseHTTPCallSteps
{
    protected IAPIRequestContext HTTPCallContext => FeatureContext.GetCurrentHTTPCallContext();

    protected HTTPCallContextSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext) { }

    protected HTTPCallContextSteps(FeatureContext featureContext) : base(featureContext) { }
}

/// <summary>
///     Make HTTP requests and receive HTTP responses using HttpClient (<a href="https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient"></a>).
///     <br/>
///     Supports in-memory calls, for purposes such as integration testing using a WebApplicationFactory.
/// </summary>
public abstract class HTTPCallClientSteps : BaseHTTPCallSteps
{
    protected HttpClient HTTPCallClient => FeatureContext.GetCurrentHTTPClient();

    protected HTTPCallClientSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext) { }

    protected HTTPCallClientSteps(FeatureContext featureContext) : base(featureContext) { }
}

public abstract class BaseHTTPCallSteps : ProtoStepCollection
{
    protected string Token => FeatureContext.GetAPIAuthenticationToken();

    protected BaseHTTPCallSteps(FeatureContext featureContext, ScenarioContext scenarioContext) : base(featureContext, scenarioContext) { }

    protected BaseHTTPCallSteps(FeatureContext featureContext) : base(featureContext) { }

    protected static string GetURI(string path)
        => URIHelpers.ResolveEndpointURI(path).AbsoluteUri;

    protected static string GetURI(string host, string path)
        => URIHelpers.ResolveEndpointURI(TestRunContext.Profile.TestRun?.PropertyBag?.Get<string>(host) ?? "localhost", path).AbsoluteUri;
}
