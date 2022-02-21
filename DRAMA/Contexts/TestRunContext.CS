namespace DRAMA.Contexts;

    public static class TestRunContext
    {
        public static readonly Profile Profile = ConfigurationParser.GetTestRunProfile();
    }
