namespace DRAMA.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class URIAttribute : Attribute
{
    public URIAttribute(string path)
        => URI = SetURI(path);

    public Uri URI { get; init; }

    private static Uri SetURI(string path)
    {
        FrontEnd configuration = TestRunContext.Profile.SystemUnderTest?.FrontEnd
                                 ?? throw new ConfigurationErrorsException($@"Front-End Test Configuration Not Found In Profile ""{TestRunContext.Profile.Name}""");

        if (configuration.Port is null && configuration.Host is not null)
            return configuration.Protocol is null
                ? new UriBuilder($@"{configuration.Host.TrimEnd('/')}/{configuration.Path?.Trim('/')}/{path.TrimStart('/')}").Uri
                : new UriBuilder($@"{configuration.Protocol}://{configuration.Host.TrimEnd('/')}/{configuration.Path?.Trim('/')}/{path.TrimStart('/')}").Uri;

        if (configuration.Port is not null)
            return new UriBuilder(configuration.Protocol, configuration.Host, (int)configuration.Port, $@"{configuration.Path?.TrimEnd('/')}/{path.TrimStart('/')}").Uri;

        string errorMessage = new StringBuilder("Unsupported Front-End Test Configuration")
            .AppendLine($@"Host: ""{configuration.Host ?? "NULL"}""")
            .AppendLine($@"Protocol: ""{configuration.Protocol ?? "NULL"}""")
            .AppendLine($@"Port: ""{configuration.Port.ToString() ?? "NULL"}""")
            .AppendLine($@"Origin: ""{configuration.Path ?? "NULL"}""")
            .ToString();

        throw new ConfigurationErrorsException(errorMessage);
    }
}
