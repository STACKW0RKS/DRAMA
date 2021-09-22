using TechTalk.SpecFlow;

namespace DRAMA.Prototypes
{
    /// <summary>
    ///     Architecturally, this class is intended to be the base class of all classes containing SpecFlow steps.
    ///     <br/>
    ///     Derived classes inheriting from this class need to always inject the test fixture's feature context and, on a per-need basis, the scenario context as well.
    ///     <br/>
    ///     This class exposes the FeatureContext and ScenarioContext properties, which hide the members of the same respective names from the SpecFlow.Steps class.
    /// </summary>
    public abstract class ProtoStep
    {
        protected FeatureContext FeatureContext { get; init; }
        protected ScenarioContext ScenarioContext { get; init; }

        protected ProtoStep(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            FeatureContext = featureContext;
            ScenarioContext = scenarioContext;
        }

        protected ProtoStep(FeatureContext featureContext)
            => FeatureContext = featureContext;
    }
}