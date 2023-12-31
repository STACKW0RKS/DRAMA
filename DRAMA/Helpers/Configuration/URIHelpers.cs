namespace DRAMA.Helpers.Configuration;

public static class URIHelpers
{
    public static Uri ResolvePageURI(string path)
        => TestRunContext.Profile.SystemUnderTest?.FrontEnd?.ResolveURI(path)
            ?? throw new ConfigurationErrorsException($@"Front-End Test Configuration Not Found In Profile ""{TestRunContext.Profile.Name}""");

    public static Uri ResolveEndpointURI(string path)
        => TestRunContext.Profile.SystemUnderTest?.API?.ResolveURI(path)
            ?? throw new ConfigurationErrorsException($@"API Test Configuration Not Found In Profile ""{TestRunContext.Profile.Name}""");

    public static Uri ResolvePageURI(string host, string path)
        => TestRunContext.Profile.SystemUnderTest?.FrontEnd?.ResolveURI(host, path)
            ?? throw new ConfigurationErrorsException($@"Front-End Test Configuration Not Found In Profile ""{TestRunContext.Profile.Name}""");

    public static Uri ResolveEndpointURI(string host, string path)
        => TestRunContext.Profile.SystemUnderTest?.API?.ResolveURI(host, path)
            ?? throw new ConfigurationErrorsException($@"API Test Configuration Not Found In Profile ""{TestRunContext.Profile.Name}""");

    private static Uri ResolveURI(this FrontEnd configuration, string path)
        => ParseURI(configuration.Host, configuration.Protocol, configuration.Port, configuration.Path, path);

    private static Uri ResolveURI(this API configuration, string path)
        => ParseURI(configuration.Host, configuration.Protocol, configuration.Port, configuration.Path, path);

    private static Uri ResolveURI(this FrontEnd configuration, string host, string path)
        => ParseURI(host, configuration.Protocol, configuration.Port, string.Empty, path);

    private static Uri ResolveURI(this API configuration, string host, string path)
        => ParseURI(host, configuration.Protocol, configuration.Port, string.Empty, path);

    private static Uri ParseURI(string? host, string? protocol, int? port, string? basePath, string? additionalPath)
    {
        if (host is not null)
        {
            host = string.IsNullOrWhiteSpace(basePath) && string.IsNullOrWhiteSpace(additionalPath) ? host : host.TrimEnd('/');

            if (port is null)
            {
                StringBuilder uri = new();

                uri.Append(protocol is null ? host : protocol + ':' + '/' + '/' + host);
                uri.Append(string.IsNullOrWhiteSpace(basePath) ? string.Empty : '/');
                uri.Append(string.IsNullOrWhiteSpace(additionalPath) ? basePath?.TrimStart('/') : basePath?.Trim('/'));
                uri.Append(string.IsNullOrWhiteSpace(additionalPath) ? string.Empty : '/');
                uri.Append(additionalPath?.TrimStart('/'));

                return new UriBuilder(uri.ToString()).Uri;
            }

            if (port is not null)
            {
                StringBuilder path = new();

                path.Append(string.IsNullOrWhiteSpace(additionalPath) ? basePath : basePath?.TrimEnd('/'));
                path.Append(string.IsNullOrWhiteSpace(additionalPath) ? string.Empty : '/');
                path.Append(additionalPath?.TrimStart('/'));

                return new UriBuilder(protocol, host, port.GetValueOrDefault(), path.ToString()).Uri;
            }
        }

        string errorMessage = new StringBuilder("Unsupported System Under Test Configuration")
            .AppendLine($@"Host: ""{host ?? "NULL"}""")
            .AppendLine($@"Protocol: ""{protocol ?? "NULL"}""")
            .AppendLine($@"Port: ""{port.ToString() ?? "NULL"}""")
            .AppendLine($@"Origin: ""{basePath ?? "NULL"}""")
            .ToString();

        throw new ConfigurationErrorsException(errorMessage);
    }
}
