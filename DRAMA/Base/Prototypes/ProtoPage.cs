namespace DRAMA.Base.Prototypes;

/// <summary>
///     Architecturally, this class is intended to be the lowest-level parent class of all page object models.
///     <br/>
///     This class exposes the Page property, which holds a reference to the Playwright page object used during initialisation of objects of derived classes.
/// </summary>
public abstract class ProtoPage
{
    /// <summary>
    ///     Constructs a proto page object from a Playwright page object.
    /// </summary>
    protected ProtoPage(IPage page)
        => Page = page;

    /// <summary>
    ///     The Playwright page object.
    ///     More information on the Playwright page API can be found at <a href="https://playwright.dev/dotnet/docs/api/class-page"></a>.
    /// </summary>
    public IPage Page { get; init; }

    /// <summary>
    ///     The title of the page.
    /// </summary>
    public string Title => Page.TitleAsync().Get();

    /// <summary>
    ///     The URI which identifies this page.
    ///     This property is set by decorating the page object model with the URI attribute.
    ///     If the page object model has not been decorated with the URI attribute, the value of this property is the current URL of the page.
    /// </summary>
    public Uri URI => GetType().GetCustomAttribute<PageURIAttribute>(false)?.URI ?? new Uri(Page.Url);

    /// <summary>
    ///     The URL at which this page is located.
    ///     This can also be used as an unescaped version of URI.AbsoluteUri (e.g. "#" will be used instead of "%23").
    /// </summary>
    public string URL => Uri.UnescapeDataString(URI.AbsoluteUri);
}
