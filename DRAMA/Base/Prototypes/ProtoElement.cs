namespace DRAMA.Base.Prototypes;

/// <summary>
///     Architecturally, this class is intended to be the lowest-level parent class of all element object models.
///     <br />
///     Inheriting from this class exposes several properties which can be used to interact with the element and/or its parent: Page, Selector, Locator.
/// </summary>
public abstract class ProtoElement
{
    /// <summary>
    ///     Constructs a proto element object from a Playwright page object parent.
    /// </summary>
    protected ProtoElement(IPage page, string selector)
    {
        Page = page;
        Selector = selector;
        Locator = page.Locator(selector);
    }

    /// <summary>
    ///     The parent Playwright page of the element.
    ///     More information on the Playwright page API can be found at <a href="https://playwright.dev/dotnet/docs/api/class-page"></a>.
    /// </summary>
    public IPage Page { get; init; }

    /// <summary>
    ///     The selector of the element.
    ///     More information on element selectors can be found at <a href="https://www.w3.org/TR/selectors/"></a>.
    /// </summary>
    public string Selector { get; init; }

    /// <summary>
    ///     The locator of the element.
    ///     More information on element locators can be found at <a href="https://playwright.dev/dotnet/docs/locators"></a>.
    /// </summary>
    public ILocator? Locator { get; init; }
}
