namespace DRAMA.Helpers.Configuration;

internal static class ConfigurationParser
{
    /// <summary>
    ///     Returns a test run profile by name from the collection of test run profiles present in the configuration file.
    ///     The default profile which will be returned is "DRAMA Default", however, this can be overridden by setting the "DRAMA_PROFILE" environment variable.
    ///     If no test run profile names in the configuration file match the value of the environment variable or the default value, then a NoMatchException is thrown.
    /// </summary>
    internal static Profile GetTestRunProfile(string environmentVariable = "DRAMA_PROFILE", string profileName = "DRAMA Default")
    {
        // If the environment variable is set, then use the environment variable for the test run profile name, otherwise use the default value.
        string testRunProfileName = Environment.GetEnvironmentVariable(environmentVariable).HasTextContent()
            ? Environment.GetEnvironmentVariable(environmentVariable)!
            : profileName;

        // Return the test run profile by name, or throw a NoMatchException if no test run profile with that name is present in the configuration file.
        Profile testRunProfile = ParseConfiguration().SingleOrDefault(profile => profile.Name is not null && profile.Name.Equals(testRunProfileName))
                                 ?? throw new NoMatchException($@"Profile ""{testRunProfileName}"" Not Found In The Configuration File");

        // If the profile's test run configuration is NULL, then create a new test run configuration for the profile.
        // This is needed because defining the profile's test run configuration in the configuration file is not mandatory. 
        testRunProfile.TestRun ??= new TestRun();

        // Set the name of the test run.
        // Unless overridden by an environment variable, a timestamp is used so that multiple test runs in the same directory can be ordered in a logical sequence.
        testRunProfile.TestRun.SetTestRunName();

        // Set the path to the directory in which the test results will be stored.
        testRunProfile.TestRun.SetTestRunResultsPath();

        // Set whether or not to skip all steps after a failed step, within the feature scope.
        // If the "Stop Feature At First Error" option is not set, default to TRUE.
        testRunProfile.TestRun.StopFeatureAtFirstError ??= true;

        // Populate the test run context with properties defined in the configuration file.
        if (testRunProfile.TestRun.PropertyBag is not null && testRunProfile.TestRun.PropertyBag.Any())
        {
            Contexts.TestRun.PropertyBag ??= new Dictionary<string, object>();

            foreach (KeyValuePair<string, object> element in testRunProfile.TestRun.PropertyBag)
                Contexts.TestRun.PropertyBag.Add(element.Key, element.Value);
        }

        // Set whether or not to log debug information, such as step/scenario/feature execution duration.
        // If the "Debug Logging" option is not set, default to FALSE.
        testRunProfile.TestRun.DebugLogging ??= false;

        // If the test run profile specifies a browser driver configuration, set whether Playwright outputs debug events to the log or not.
        testRunProfile.SystemUnderTest?.FrontEnd?.BrowserDriver?.SetDebugLogging();

        // Return the test run profile, after having set values for the profile's mandatory properties.
        return testRunProfile;
    }

    /// <summary>
    ///     Returns the collection of test run profiles discovered in the configuration file, or throws a NoMatchException if no test run profiles are discovered.
    ///     The default configuration file name that this method looks for is "DRAMA.JSON", which needs to exist in the solution's output directory.
    ///     The configuration file is validated against the configuration schema defined in file "DRAMA.JSON.SCHEMA", which gets copied to the solution's output directory at build time.
    /// </summary>
    private static IEnumerable<Profile> ParseConfiguration(string configurationFile = "DRAMA.JSON", string schemaFile = "DRAMA.JSON.SCHEMA")
    {
        // Set the configuration file, allowing for case-insensitive matches.
        configurationFile = (new DirectoryInfo(Environment.CurrentDirectory).GetFiles().SingleOrDefault(file => file.Name.Equals(configurationFile, StringComparison.OrdinalIgnoreCase))
            ?? throw new FileNotFoundException($@"Configuration File ""{configurationFile}"" (Case-Insensitive Name) Not Found In Directory ""{Environment.CurrentDirectory}""")).FullName;

        // Set the schema file, allowing for case-insensitive matches.
        schemaFile = (new DirectoryInfo(Environment.CurrentDirectory).GetFiles().SingleOrDefault(file => file.Name.Equals(schemaFile, StringComparison.OrdinalIgnoreCase))
            ?? throw new FileNotFoundException($@"Configuration File Schema ""{schemaFile}"" (Case-Insensitive Name) Not Found In Directory ""{Environment.CurrentDirectory}""")).FullName;

        // Read the configuration file, and store the content in memory.
        string configurationFileContent = File.ReadAllText(configurationFile);

        // Read the schema for the configuration file, and store the content in memory.
        string schemaFileContent = File.ReadAllText(schemaFile);

        // Validate the configuration against the configuration schema.
        if (JObject.Parse(configurationFileContent).IsValid(JSchema.Parse(schemaFileContent)).Equals(false))
            throw new JSchemaException($@"Configuration File ""{configurationFile}"" Does Not Validate Against The ""{schemaFile}"" Configuration Schema");

        // Deserialise the content of the configuration file.
        Models.Configuration.Configuration configuration = JsonConvert.DeserializeObject<Models.Configuration.Configuration>(configurationFileContent) ?? throw new Newtonsoft.Json.JsonException("Unable To Deserialise Configuration File Content");

        // If no test run profiles have been discovered, throw an exception.
        if (configuration?.TestRunProfiles is null || configuration.TestRunProfiles.None())
            throw new NoMatchException($@"No Test Run Profiles Found In Configuration File ""{configurationFile}""");

        // Return the collection of discovered test run profiles.
        return configuration.TestRunProfiles;
    }

    /// <summary>
    ///     Sets the name of the test run.
    ///     If the "DRAMA_TEST_RUN_NAME" environment variable is set, then use the value of that environment variable for the test run name.
    ///     Otherwise, generate a new test run name formatted as a timestamp.
    /// </summary>
    private static void SetTestRunName(this TestRun run, string testRunNameEnvironmentVariable = "DRAMA_TEST_RUN_NAME")
        => run.Name = Environment.GetEnvironmentVariable(testRunNameEnvironmentVariable).HasTextContent()
            ? Environment.GetEnvironmentVariable(testRunNameEnvironmentVariable)!
            : $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}";

    /// <summary>
    ///     Sets the path to the directory in which the test results will be stored. The parent of this directory is the "DRAMA_RESULTS" directory.
    ///     The name of this directory can be overridden by setting the "DRAMA_TEST_RUN_NAME" environment variable.
    ///     The default base path is the solution's output directory, however, this can be overridden by setting the "DRAMA_RESULTS_PATH" environment variable.
    /// </summary>
    private static void SetTestRunResultsPath(this TestRun run, string resultsPathEnvironmentVariable = "DRAMA_RESULTS_PATH", string directoryName = "DRAMA_RESULTS")
    {
        // If the test run name has not been set, throw an exception.
        if (run.Name is null) throw new ConfigurationErrorsException("The Test Run Name Has Not Been Set");

        // If the results path environment variable is set, then use that environment variable for the results path.
        // Otherwise, if the value of the "Results Path" key in the configuration file is either NULL or "DEFAULT" (case insensitive), then use the solution's output directory.
        // Otherwise, use the value of the key in the configuration file for the path.
        string resultsPath = Environment.GetEnvironmentVariable(resultsPathEnvironmentVariable).HasTextContent()
            ? Path.Combine(Environment.GetEnvironmentVariable(resultsPathEnvironmentVariable)!, directoryName, run.Name)
            : run.ResultsPath is null || run.ResultsPath.Flatten().Equals("DEFAULT")
                ? Path.Combine(directoryName, run.Name)
                : Path.Combine(
                    Uri.IsWellFormedUriString(run.ResultsPath, UriKind.RelativeOrAbsolute)
                        ? run.ResultsPath
                        : throw new UriFormatException($@"""{run.ResultsPath}"" Is Not A Valid URI For The Results Path"),
                    directoryName, run.Name);

        // Append a directory separator character to the path before setting it.
        // The directory separator character is "\" on Windows and "/" on UNIX-based systems.
        run.ResultsPath = resultsPath + Path.DirectorySeparatorChar;
    }

    /// <summary>
    ///     Sets whether Playwright outputs debug events to the log or not.
    ///     The default value is FALSE. Additional information can be found at <a href="https://playwright.dev/dotnet/docs/debug#verbose-api-logs"></a>.
    /// </summary>
    private static void SetDebugLogging(this BrowserDriver browserDriver)
    {
        // Remove the "DEBUG" environment variable from all potential targets, in order to avoid conflicts.
        foreach (EnvironmentVariableTarget target in new[] { EnvironmentVariableTarget.Machine, EnvironmentVariableTarget.User, EnvironmentVariableTarget.Process })
            if (Environment.GetEnvironmentVariable("DEBUG", target) is not null) Environment.SetEnvironmentVariable("DEBUG", null, target);

        // If the value of the "Debug Logging" key in the configuration file is TRUE, then enable Playwright to output debug events to the log.
        if (browserDriver.DebugLogging ?? false) Environment.SetEnvironmentVariable("DEBUG", "pw:api", EnvironmentVariableTarget.Process);
    }
}
