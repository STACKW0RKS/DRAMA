namespace DRAMA.Helpers.Configuration;

internal static class ConfigurationParser
{
    internal static Profile GetTestRunProfile(string profileName = "DRAMA Default")
    {
        string testRunProfileName = Environment.GetEnvironmentVariable("DRAMA Profile").HasTextContent() ? Environment.GetEnvironmentVariable("DRAMA Profile")! : profileName;

        Profile? testRunProfile = ParseConfiguration().Where(profile => profile.Key.Equals(testRunProfileName)).Select(profile => profile.Value).SingleOrDefault();

        if (testRunProfile is null) throw new NoMatchException($@"Profile ""{testRunProfileName}"" Not Found In Configuration File");

        testRunProfile.Name = testRunProfileName;

        return EnrichProfile(testRunProfile);
    }

    private static Dictionary<string, Profile> ParseConfiguration(string configurationFile = "DRAMA.JSON")
    {
        string configurationFileContent = File.ReadAllText(configurationFile);

        Dictionary<string, Profile> testRunProfiles = JsonConvert.DeserializeObject<Dictionary<string, Profile>>(configurationFileContent);

        if (testRunProfiles.None()) throw new ConfigurationErrorsException($@"No Test Run Profiles Found In Configuration File ""{configurationFile}""");

        return testRunProfiles;
    }

    private static Profile EnrichProfile(Profile profile)
    {
        profile.TestRun ??= new TestRun();
        profile.TestRun.Name = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}";

        profile.TestRun.ResultsPath = SetTestRunResultsPath(profile.TestRun) + Path.DirectorySeparatorChar;

        return profile;
    }

    private static string SetTestRunResultsPath(TestRun run)
        => Environment.GetEnvironmentVariable("DRAMA Results Path").HasTextContent()
            ? Path.Combine(Environment.GetEnvironmentVariable("DRAMA Results Path")!, "DRAMA Results", run.Name)
            : run.ResultsPath is null || run.ResultsPath.Flatten().Equals("DEFAULT")
                ? Path.Combine("DRAMA Results", run.Name)
                : Path.Combine(run.ResultsPath, "DRAMA Results", run.Name);



    // TODO: Add Comments To All Of This
}
