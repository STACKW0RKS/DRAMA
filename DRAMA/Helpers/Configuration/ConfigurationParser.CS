using System.Configuration;
using DRAMA.Models.Configuration;
using DRAMA.Models.TargetFramework;

namespace DRAMA.Helpers.Configuration;

internal static class ConfigurationParser
{
    public static async Task<Profile> GetTestExecutionProfile(string profileName = "DRAMA Default")
    {
        string testRunProfileName = Environment.GetEnvironmentVariable("DRAMA Profile").HasTextContent() ? Environment.GetEnvironmentVariable("DRAMA Profile")! : profileName;

        Profile? testRunProfile = (await ParseConfiguration()).Where(profile => profile.Key.Equals(testRunProfileName)).Select(profile => profile.Value).SingleOrDefault();

        if (testRunProfile is null) throw new NoMatchException($@"Profile ""{testRunProfileName}"" Not Found In Configuration File");

        return EnrichProfile(testRunProfile);
    }

    private static async Task<Dictionary<string, Profile>> ParseConfiguration(string configurationFile = "DRAMA.JSON")
    {
        string configurationFileContent = await File.ReadAllTextAsync(configurationFile);

        Dictionary<string, Profile> testRunProfiles = JsonConvert.DeserializeObject<Dictionary<string, Profile>>(configurationFileContent);

        if (testRunProfiles.None()) throw new ConfigurationErrorsException($@"No Test Run Profiles Found In Configuration File ""{configurationFile}""");

        return testRunProfiles;
    }

    private static Profile EnrichProfile(Profile profile)
    {
        profile.TestRun ??= new TestRun();

        profile.TestRun.ResultsPath = SetTestRunResultsPath(profile);
        profile.TestRun.Origin = SetTestRunOrigin(profile);

        return profile;
    }

    private static string SetTestRunResultsPath(Profile profile)
        => Environment.GetEnvironmentVariable("DRAMA Results Path").HasTextContent()
            ? Path.Combine(Environment.GetEnvironmentVariable("DRAMA Results Path")!, "DRAMA Results", profile.TestRun!.Name)
            : profile.TestRun!.ResultsPath is null || profile.TestRun!.ResultsPath.Flatten().Equals("DEFAULT")
                ? Path.Combine("DRAMA Results", profile.TestRun!.Name)
                : Path.Combine(profile.TestRun!.ResultsPath, "DRAMA Results", profile.TestRun!.Name);

    private static Uri? SetTestRunOrigin(Profile profile)
    {
        SystemUnderTest SUT = profile.SystemUnderTest!;

        if (SUT.Origin is not null && !SUT.Origin.First().Equals('/')) SUT.Origin = '/' + SUT.Origin;

        if (SUT.Host is null && SUT.Protocol is null && SUT.Port is null && SUT.Origin is null) return null;
        if (SUT.Port is not null) return new UriBuilder(SUT.Protocol, SUT.Host, (int)SUT.Port, SUT.Origin).Uri;
        if (SUT.Host is not null && SUT.Protocol is null && SUT.Port is null && SUT.Origin is null) return new UriBuilder(SUT.Host).Uri;
        if (SUT.Host is not null && SUT.Protocol is null && SUT.Port is null && SUT.Origin is not null) return new UriBuilder($@"{SUT.Host + SUT.Origin}").Uri;

        string errorMessage = new StringBuilder("Unsupported System Under Test Configuration")
            .AppendLine($@"Connection String: ""{SUT.ConnectionString ?? "NULL"}""")
            .AppendLine($@"Host: ""{SUT.Host ?? "NULL"}""")
            .AppendLine($@"Protocol: ""{SUT.Protocol ?? "NULL"}""")
            .AppendLine($@"Port: ""{SUT.Port.ToString() ?? "NULL"}""")
            .AppendLine($@"Origin: ""{SUT.Origin ?? "NULL"}""")
            .ToString();

        throw new ConfigurationErrorsException(errorMessage);
    }

    // TODO: Add Comments To All Of This

    /*
    public static void SetFrameworkSettings(string config)
    {
        if (Z34.TestRunProfile.Contains("SQLite"))
            Z34.ConnectionStringBuilder = new SqliteConnectionStringBuilder(configBindings.ConnectionString);

        if (Z34.TestRunProfile.Contains("Oracle"))
            Z34.ConnectionStringBuilder = configBindings.ConnectionString; // ¯\_(ヅ)_/¯
    }
    */
}
