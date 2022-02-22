using DRAMA.Contexts;

namespace DRAMA.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class URIAttribute : Attribute
{
    public URIAttribute(string path)
        => URI = SetURI();

    public Uri? URI { get; init; }

    private Uri? SetURI()
    {
        Profile profile = TestRunContext.Profile;
        FrontEnd configuration = profile.SystemUnderTest?.FrontEnd
                                  ?? throw new ConfigurationErrorsException($@"Front-End Test Configuration Not Found In Profile ""{profile.Name}""");

        if (configuration.Host is null && configuration.Protocol is null && configuration.Port is null && configuration.Path is null) return null;
        if (configuration.Host is not null && configuration.Protocol is null && configuration.Port is null) return new UriBuilder($@"{configuration.Host}/{configuration.Path?.TrimStart('/')}").Uri;
        if (configuration.Port is not null) return new UriBuilder(configuration.Protocol, configuration.Host, (int)configuration.Port, configuration.Path).Uri;

        string errorMessage = new StringBuilder("Unsupported Front-End Test Configuration")
            .AppendLine($@"Host: ""{configuration.Host ?? "NULL"}""")
            .AppendLine($@"Protocol: ""{configuration.Protocol ?? "NULL"}""")
            .AppendLine($@"Port: ""{configuration.Port.ToString() ?? "NULL"}""")
            .AppendLine($@"Origin: ""{configuration.Path ?? "NULL"}""")
            .ToString();

        throw new ConfigurationErrorsException(errorMessage);
    }
}

// TODO: Make Most Classes Sealed?
