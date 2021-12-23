namespace DRAMA.Prototypes;

/// <summary>
///     Architecturally, this class is intended to be the base class of all page object models.
///     <br />
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
    ///     More information on the Playwright page API can be found at <a href="https://playwright.dev/dotnet/docs/next/api/class-page"></a>.
    /// </summary>
    public IPage Page { get; init; }

    /// <summary>
    ///     The title of the page.
    /// </summary>
    public string Title => Page.TitleAsync().GetAwaiter().GetResult();

    /// <summary>
    ///     The URL of the page.
    /// </summary>
    public string URL => Page.Url;
}
