namespace DRAMA.Base.Prototypes;

/// <summary>
///     Architecturally, this class is intended to be the lowest-level parent class of all classes containing SpecFlow steps.
///     <br/>
///     This class exposes the FeatureContext and ScenarioContext properties, which hide the members of the same respective names from the SpecFlow.Steps class.
/// </summary>
public abstract class ProtoStepCollection
{
    /// <summary>
    ///     Constructs a proto step collection object by injecting both the SpecFlow feature context and the SpecFlow scenario context as dependencies.
    /// </summary>
    protected ProtoStepCollection(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        FeatureContext = featureContext;
        ScenarioContext = scenarioContext;
    }

    /// <summary>
    ///     Constructs a proto step collection object by injecting the SpecFlow feature context as a dependency.
    /// </summary>
    protected ProtoStepCollection(FeatureContext featureContext)
        => FeatureContext = featureContext;

    /// <summary>
    ///     Constructs a proto step collection object by injecting the SpecFlow scenario context as a dependency.
    /// </summary>
    protected ProtoStepCollection(ScenarioContext scenarioContext)
        => ScenarioContext = scenarioContext;

    /// <summary>
    ///     Constructs a proto step collection object by injecting none of the SpecFlow contexts as dependencies.
    /// </summary>
    protected ProtoStepCollection() { }

    /// <summary>
    ///     The SpecFlow feature context.
    ///     More information can be found at <a href="https://docs.specflow.org/projects/specflow/en/latest/Bindings/FeatureContext.html"></a>.
    /// </summary>
    public FeatureContext FeatureContext { get; init; } = null!;

    /// <summary>
    ///     The SpecFlow scenario context.
    ///     More information can be found at <a href="https://docs.specflow.org/projects/specflow/en/latest/Bindings/ScenarioContext.html"></a>.
    /// </summary>
    public ScenarioContext ScenarioContext { get; init; } = null!;
}
