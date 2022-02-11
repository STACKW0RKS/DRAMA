namespace DRAMA.Models.TestRun;

[Serializable]
internal class Configuration
{
    internal Configuration()
    {
        SystemUnderTest = new UriBuilder();

        if (Host is not null) SystemUnderTest.Host = Host;
        if (Protocol is not null) SystemUnderTest.Scheme = Protocol;
        if (Port is not null) SystemUnderTest.Port = (int)Port;
        if (LoginEndpoint is not null) SystemUnderTest.Path = LoginEndpoint;
    }

    internal string? ResultsPath { get; set; }

    internal UriBuilder SystemUnderTest { get; set; }

    internal string? Browser { get; set; }

    internal BrowserTypeLaunchOptions? BrowserOptions { get; set; }

    internal string? ConnectionString { get; set; }

    private string? Host { get; set; }

    private string? Protocol { get; set; }

    private int? Port { get; set; }

    private string? LoginEndpoint { get; set; }
}
