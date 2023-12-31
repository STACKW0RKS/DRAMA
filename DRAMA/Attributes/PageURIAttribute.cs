namespace DRAMA.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class PageURIAttribute : Attribute
{
    public PageURIAttribute(string path)
        => URI = URIHelpers.ResolvePageURI(path);

    public PageURIAttribute(string host, string path)
        => URI = URIHelpers.ResolvePageURI(TestRunContext.Profile.TestRun?.PropertyBag?.Get<string>(host) ?? "localhost", path);

    public Uri URI { get; init; }
}
