using DRAMA.Extensions.Specflow;

namespace DRAMA.EventBindings;

[Binding]
public class CommonFixtureBindings
{
    [BeforeTestRun(Order = 0)]
    public static void BeforeTestRun(TestThreadContext testThreadContext)
        => testThreadContext.SetTestRunProfile(ConfigurationParser.GetTestRunProfile());
}
