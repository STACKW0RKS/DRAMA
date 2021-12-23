namespace DRAMA.Prototypes;

/// <summary>
///     Architecturally, this class is intended to be the base class of all classes containing SpecFlow steps.
///     <br />
///     Derived classes inheriting from this class need to always inject the test fixture's feature context and, on a per-need basis, the scenario context as well.
///     <br />
///     This class exposes the FeatureContext and ScenarioContext properties, which hide the members of the same respective names from the SpecFlow.Steps class.
/// </summary>
public abstract class ProtoStep
{
    /// <summary>
    ///     Constructs a proto step object by injecting both the SpecFlow feature context and the SpecFlow scenario context as dependencies.
    /// </summary>
    protected ProtoStep(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        FeatureContext = featureContext;
        ScenarioContext = scenarioContext;
    }

    /// <summary>
    ///     Constructs a proto step object by injecting the SpecFlow scenario context as a dependency.
    /// </summary>
    protected ProtoStep(FeatureContext featureContext)
        => FeatureContext = featureContext;

    /// <summary>
    ///     The SpecFlow feature context.
    ///     More information can be found at <a href="https://docs.specflow.org/projects/specflow/en/latest/Bindings/FeatureContext.html"></a>.
    /// </summary>
    protected FeatureContext FeatureContext { get; init; }

    /// <summary>
    ///     The SpecFlow scenario context.
    ///     More information can be found at <a href="https://docs.specflow.org/projects/specflow/en/latest/Bindings/ScenarioContext.html"></a>.
    /// </summary>
    protected ScenarioContext ScenarioContext { get; init; }
}
