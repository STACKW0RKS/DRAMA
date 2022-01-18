namespace DRAMA.Prototypes;

/// <summary>
///     Architecturally, this class is intended to be the base class of all complex elements, complex meaning that the element is implemented as a class rather than an element handle.
///     <br />
///     Inheriting from this class exposes several properties which can be used to interact with the element and/or its parent: Page, ElementSelector, ElementLocator, ElementHandle.
/// </summary>
public abstract class ProtoElement
{
    /// <summary>
    ///     <para>
    ///         Constructs an element object from a Playwright page object parent.
    ///         <br />
    ///         The "initialiseElementHandle" parameter of this constructor defines whether the element is static and can be initialised by the constructor, or whether the element is dynamic and needs to be initialised at a later point in time.
    ///     </para>
    ///     <para>
    ///         Static elements are defined as elements which always exist in the DOM (including the shadow DOM) and are always visible.
    ///         <br />
    ///         Dynamic elements are defined as elements which only start existing in the DOM (including the shadow DOM) and that only become visible after certain events are triggered, e.g. clicking a button causes a modal window to pop up on screen.
    ///     </para>
    ///     <para>
    ///         Parents of dynamic elements need to implement the Initialise() and Dispose() members, in order to avoid null reference exceptions. These parents can be either page objects or other element objects.
    ///         <br />
    ///         e.g. OnElementInitialisation() => Element.Initialise(); OnElementTermination() => Element.Dispose();
    ///     </para>
    /// </summary>
    protected ProtoElement(IPage page, string selector, bool initialiseElementHandle)
    {
        Page = page;
        ElementSelector = selector;
        ElementLocator = page.Locator(selector);

        if (initialiseElementHandle)
            Initialise().GetAwaiter().GetResult();
    }

    /// <summary>
    ///     Constructs an element object from a Playwright element handle object parent.
    /// </summary>
    protected ProtoElement(IElementHandle parentElement, string selector)
    {
        Page = parentElement.OwnerFrameAsync().GetAwaiter().GetResult()?.Page;
        ElementSelector = selector;
        ElementLocator = Page?.Locator(selector);
        ElementHandle = parentElement.QuerySelectorAsync(selector).GetAwaiter().GetResult();
    }

    /// <summary>
    ///     The parent Playwright page of the element.
    ///     More information on the Playwright page API can be found at <a href="https://playwright.dev/dotnet/docs/next/api/class-page"></a>.
    /// </summary>
    public IPage? Page { get; init; }

    /// <summary>
    ///     The selector of the element.
    ///     More information on element selectors can be found at <a href="https://playwright.dev/dotnet/docs/next/selectors"></a>.
    /// </summary>
    public string ElementSelector { get; init; }

    /// <summary>
    ///     The locator of the element.
    ///     More information on element locators can be found at <a href="https://playwright.dev/dotnet/docs/next/locators"></a>.
    /// </summary>
    public ILocator? ElementLocator { get; init; }

    /// <summary>
    ///     The handle of the element.
    ///     More information on the Playwright element handle API can be found at <a href="https://playwright.dev/dotnet/docs/next/api/class-elementhandle/"></a>.
    /// </summary>
    public IElementHandle? ElementHandle { get; private set; }

    /// <summary>
    ///     Initialises a dynamic element.
    ///     This method should always be called once the dynamic element has started existing in the DOM (including the shadow DOM).
    ///     <br />
    ///     Can also be used to reinitialise either a static or a dynamic element after it's been disposed of.
    /// </summary>
    public async Task Initialise()
    {
        if (Page is not null)
        {
            await Page.WaitForSelectorAsync(ElementSelector);
            ElementHandle = await Page.QuerySelectorAsync(ElementSelector);
        }

        else ElementHandle = null;
    }

    /// <summary>
    ///     Disposes of the element.
    ///     The element can be reinitialised, if it still exists in the DOM (including the shadow DOM).
    /// </summary>
    public async Task Dispose()
    {
        if (ElementHandle is not null) await ElementHandle.DisposeAsync();
    }
}
