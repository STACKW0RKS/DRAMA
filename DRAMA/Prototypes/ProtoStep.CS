using TechTalk.SpecFlow;

namespace DRAMA.Prototypes
{
    internal class ProtoStep
    {
        protected FeatureContext FeatureContext;
        protected ScenarioContext ScenarioContext;

        protected ProtoStep(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            FeatureContext = featureContext;
            ScenarioContext = scenarioContext;
        }

        protected ProtoStep(FeatureContext featureContext)
            => FeatureContext = featureContext;
    }
}