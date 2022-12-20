namespace DRAMA.Contexts;

public static class TestRunContext
{
    public static readonly Profile Profile = ConfigurationParser.GetTestRunProfile();

    /// <summary>
    ///     The local date and time at which the test run started.
    ///     The "ToUniversalTime" method can be used to convert to UTC.
    /// </summary>
    public static readonly DateTime StartDateTime = DateTime.Now;

    public static Dictionary<string, object>? PropertyBag { get; set; }
}
