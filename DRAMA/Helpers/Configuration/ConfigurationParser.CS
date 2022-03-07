namespace DRAMA.Helpers.Configuration;

internal static class ConfigurationParser
{
    /// <summary>
    ///     The name of the test run.
    ///     The value of this property is generated at runtime.
    /// </summary>
    private static string TestRunName => $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}";

    /// <summary>
    ///     Returns a test run profile by name from the collection of test run profiles present in the configuration file.
    ///     The default profile which will be returned is "DRAMA Default", however, this can be overridden by setting the "DRAMA_PROFILE" environment variable.
    ///     If no test run profile names in the configuration file match the value of the environment variable or the default value, then a NoMatchException is thrown.
    /// </summary>
    internal static Profile GetTestRunProfile(string environmentVariable = "DRAMA_PROFILE", string profileName = "DRAMA Default")
    {
        // If the environment variable is set, then use the environment variable for the test run profile name, otherwise use the default value.
        string testRunProfileName = Environment.GetEnvironmentVariable(environmentVariable).HasTextContent()
            ? Environment.GetEnvironmentVariable(environmentVariable) ?? profileName
            : profileName;

        // Return the test run profile by name, or throw a NoMatchException if no test run profile with that name are present in the configuration file.
        Profile testRunProfile = ParseConfiguration().SingleOrDefault(profile => profile.Key.Equals(testRunProfileName)).Value
                                 ?? throw new NoMatchException($@"Profile ""{testRunProfileName}"" Not Found In Configuration File");

        // Set the name of the test run profile from the key under which the profile was stored in the dictionary of test run profiles.
        // This is the more elegant approach, as the entire configuration for a single profile can be the value of the key with the profile's name.
        // The alternative would be to have a list of unnamed profile objects, each having a "Profile Name" (or similar) key-value pair to identify the profile by.
        testRunProfile.Name = testRunProfileName;

        // If the profile's test run configuration is NULL, then create a new test run configuration for the profile.
        // This is needed because defining the profile's test run configuration in the configuration file is not mandatory. 
        testRunProfile.TestRun ??= new TestRun();

        // Set the name of the test run. A timestamp is used so that multiple test runs in the same directory can be ordered in a logical sequence.
        testRunProfile.TestRun.Name = TestRunName;

        // Set the path to the directory in which the test results will be stored.
        testRunProfile.TestRun.ResultsPath = testRunProfile.TestRun.SetTestRunResultsPath();

        // If the "Stop Feature At First Error" option is not set, default to FALSE.
        testRunProfile.TestRun.StopFeatureAtFirstError ??= true;

        // If the test run profile specifies browser driver configuration, set whether Playwright outputs debug events to the log or not.
        testRunProfile.BrowserDriver?.SetDebugLogging();

        // Return the test run profile, after having set values for the profile's mandatory properties.
        return testRunProfile;
    }

    /// <summary>
    ///     Returns the collection of test run profiles discovered in the configuration file, or throws a NoMatchException if no test run profiles are discovered.
    ///     The default configuration file name that this method looks for is "DRAMA.JSON", which needs to exist in the solution's output directory.
    /// </summary>
    private static Dictionary<string, Profile> ParseConfiguration(string configurationFile = "DRAMA.JSON")
    {
        // Read the configuration file and store the content in memory.
        string configurationFileContent = File.ReadAllText(configurationFile);

        // Deserialise the in-memory content of the configuration file to a dictionary of test run profiles, the keys of which are the profile names.
        Dictionary<string, Profile> testRunProfiles = JsonConvert.DeserializeObject<Dictionary<string, Profile>>(configurationFileContent);

        // If no test run profiles have been discovered in the in-memory content of the configuration file, throw an exception.
        if (testRunProfiles.None()) throw new NoMatchException($@"No Test Run Profiles Found In Configuration File ""{configurationFile}""");

        // Return the dictionary of test run profiles.
        return testRunProfiles;
    }

    /// <summary>
    ///     Returns the path to the directory in which the test results will be stored. The parent of this directory is the "DRAMA_RESULTS" directory.
    ///     The name of this directory can be overridden by setting the "DRAMA_TEST_RUN_NAME" environment variable.
    ///     The default base path is the solution's output directory, however, this can be overridden by setting the "DRAMA_RESULTS_PATH" environment variable.
    /// </summary>
    private static string SetTestRunResultsPath // TODO: Make The Parameters Configurable
        (this TestRun run, string resultsPathEnvironmentVariable = "DRAMA_RESULTS_PATH", string directoryName = "DRAMA_RESULTS", string testRunNameEnvironmentVariable = "DRAMA_TEST_RUN_NAME")
    {
        // If the test run name environment variable is set, then use that environment variable for the test run name.
        // Otherwise, use the test run name generated at runtime.
        // If the test run name is NULL, generate a new test run name and use that.
        string testRunName = Environment.GetEnvironmentVariable(testRunNameEnvironmentVariable).HasTextContent()
            ? Environment.GetEnvironmentVariable(testRunNameEnvironmentVariable)!
            : run.Name ??= TestRunName;

        // If the results path environment variable is set, then use that environment variable for the results path.
        // Otherwise, if the value of the "Results Path" key in the configuration file is either NULL or "DEFAULT" (case insensitive), then use the solution's output directory.
        // Otherwise, use the value of the key in the configuration file for the path.
        string resultsPath = Environment.GetEnvironmentVariable(resultsPathEnvironmentVariable).HasTextContent()
            ? Path.Combine(Environment.GetEnvironmentVariable(resultsPathEnvironmentVariable)!, directoryName, testRunName)
            : run.ResultsPath is null || run.ResultsPath.Flatten().Equals("DEFAULT")
                ? Path.Combine(directoryName, testRunName)
                : Path.Combine(
                    Uri.IsWellFormedUriString(run.ResultsPath, UriKind.RelativeOrAbsolute)
                        ? run.ResultsPath
                        : throw new UriFormatException($@"""{run.ResultsPath} Is Not A Valid URI For The Results Path"""),
                    directoryName, testRunName);

        // Append a directory separator character to the path before returning it.
        // The directory separator character is "\" on Windows and "/" on UNIX-based systems.
        return resultsPath + Path.DirectorySeparatorChar;
    }

    /// <summary>
    ///     Sets whether Playwright outputs debug events to the log or not.
    ///     The default value is FALSE. Additional information can be found at <a href="https://playwright.dev/dotnet/docs/debug#verbose-api-logs"></a>.
    /// </summary>
    private static void SetDebugLogging(this BrowserDriver browserDriver)
    {
        foreach (EnvironmentVariableTarget target in new[] { EnvironmentVariableTarget.Machine, EnvironmentVariableTarget.User, EnvironmentVariableTarget.Process })
            if (Environment.GetEnvironmentVariable("DEBUG", target) is not null) Environment.SetEnvironmentVariable("DEBUG", null);

        if (browserDriver.DebugLogging ?? false) Environment.SetEnvironmentVariable("DEBUG", "pw:api", EnvironmentVariableTarget.Process);
    }
}
