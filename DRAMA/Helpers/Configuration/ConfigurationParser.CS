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

        profile.TestRun.ResultsPath = SetTestRunResultsPath(profile.TestRun) + Path.DirectorySeparatorChar;

        // TODO: The Origin Only Applies To Front-End And API Tests, So Maybe It Should Not Be In The Test Run Object
        profile.TestRun.Origin = SetTestRunOrigin(profile.SystemUnderTest?.FrontEnd ?? throw new ConfigurationErrorsException($@"Front-End Test Configuration Not Found In Profile ""{profile.Name}"""));

        return profile;
    }

    private static string SetTestRunResultsPath(TestRun run)
        => Environment.GetEnvironmentVariable("DRAMA Results Path").HasTextContent()
            ? Path.Combine(Environment.GetEnvironmentVariable("DRAMA Results Path")!, "DRAMA Results", run.Name)
            : run.ResultsPath is null || run.ResultsPath.Flatten().Equals("DEFAULT")
                ? Path.Combine("DRAMA Results", run.Name)
                : Path.Combine(run.ResultsPath, "DRAMA Results", run.Name);

    private static Uri? SetTestRunOrigin(FrontEnd layer)
    {
        if (layer.Host is null && layer.Protocol is null && layer.Port is null && layer.Origin is null) return null;
        if (layer.Host is not null && layer.Protocol is null && layer.Port is null) return new UriBuilder($@"{layer.Host}/{layer.Origin?.TrimStart('/')}").Uri;
        if (layer.Port is not null) return new UriBuilder(layer.Protocol, layer.Host, (int)layer.Port, layer.Origin).Uri;

        string errorMessage = new StringBuilder("Unsupported Front-End Test Configuration")
            .AppendLine($@"Host: ""{layer.Host ?? "NULL"}""")
            .AppendLine($@"Protocol: ""{layer.Protocol ?? "NULL"}""")
            .AppendLine($@"Port: ""{layer.Port.ToString() ?? "NULL"}""")
            .AppendLine($@"Origin: ""{layer.Origin ?? "NULL"}""")
            .ToString();

        throw new ConfigurationErrorsException(errorMessage);
    }

    // TODO: Add Comments To All Of This
}
