using System.Text;

namespace DRAMA.Helpers.Configuration;

internal class ConfigurationParser
{
    public ConfigurationParser(string configuration = "DRAMA.JSON")
    {
        
    }

    public static void SetFrameworkSettings(string config)
    {
        string configurationFilePath = new StringBuilder(Directory.GetCurrentDirectory()).ToString();

        ConfigurationBindings configBindings = new ConfigurationBuilder()
            .SetBasePath(configurationFilePath)
            .AddJsonFile("Z34.JSON")
            .Build()
            .GetSection(config)
            .Get<ConfigurationBindings>();

        Z34.Host.Base = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("Host"))
            ? configBindings.Host
            : Environment.GetEnvironmentVariable("Host");

        Z34.Protocol = configBindings.Protocol;

        Z34.Ports.QH = configBindings.Ports["QH"];
        Z34.Ports.BI = configBindings.Ports["BI"];

        if (Z34.Ports.QH.Equals(string.Empty)) Z34.Host.QH = new UriBuilder(Z34.Protocol, Z34.Host.Base).ToString();
        else Z34.Host.QH = new UriBuilder(Z34.Protocol, Z34.Host.Base, int.Parse(Z34.Ports.QH)).ToString();

        if (Z34.Ports.BI.Equals(string.Empty)) Z34.Host.BI = new UriBuilder(Z34.Protocol, Z34.Host.Base).ToString();
        else Z34.Host.BI = new UriBuilder(Z34.Protocol, Z34.Host.Base, int.Parse(Z34.Ports.BI)).ToString();

        Z34.LoginURL = new StringBuilder(Z34.Host.QH).Append(configBindings.LoginURL).ToString();

        Z34.TestRunName = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TestRunName"))
            ? $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}"
            : Environment.GetEnvironmentVariable("TestRunName");

        Z34.TestResultsDirectory = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TestResultsDirectory"))
            ? configBindings.TestResultsDirectory.Equals("default")
                ? Path.Combine(Environment.CurrentDirectory, "Z34 Test Results", Z34.TestRunName)
                : Path.Combine(configBindings.TestResultsDirectory, Z34.TestRunName)
            : Path.Combine(Environment.GetEnvironmentVariable("TestResultsDirectory"), Z34.TestRunName);

        Z34.TestRunBrowser = configBindings.TestRunBrowser;
        Z34.TestRunLaunchOptions = configBindings.TestRunLaunchOptions;

        if (Z34.TestRunProfile.Contains("SQLite"))
            Z34.ConnectionStringBuilder = new SqliteConnectionStringBuilder(configBindings.ConnectionString);

        if (Z34.TestRunProfile.Contains("Oracle"))
            Z34.ConnectionStringBuilder = configBindings.ConnectionString; // ¯\_(ヅ)_/¯
    }
}
