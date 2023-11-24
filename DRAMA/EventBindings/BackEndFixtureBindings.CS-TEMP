namespace DRAMA.EventBindings;

[Binding]
public sealed class BackEndFixtureBindings
{
    [BeforeFeature("Back-End", Order = 2)]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        featureContext.SetDatabaseConnection(BackEndHelpers.CreateDatabaseConnection());
        featureContext.GetDatabaseConnection().Open();
    }

    [AfterFeature("Back-End", Order = 2)]
    public static void AfterFeature(FeatureContext featureContext)
    {
        featureContext.GetDatabaseConnection().Close();
    }
}
