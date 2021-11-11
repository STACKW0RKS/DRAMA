namespace DRAMA.Prototypes;

/// <summary>
///     Architecturally, this class is intended to be the base class of all complex elements, complex meaning that the element is implemented as a class rather than an element handle.
///     <br/>
///     Inheriting from this class exposes several properties which can be used to interact with the element and/or its parent: Page, ElementSelector, ElementLocator, ElementHandle.
/// </summary>
public abstract class ProtoElement
{
    /// <summary>
    ///     <para>
    ///         Initialises an element object from a page object parent.
    ///         <br/>
    ///         The "initialiseElementHandle" parameter of this constructor defines whether the element is static and can be initialised by the constructor, or whether the element is dynamic and needs to be initialised at a later point in time.
    ///     </para>
    ///     <para>
    ///         Static elements are defined as elements which always exist in the (shadow) DOM and are always visible.
    ///         <br/>
    ///         Dynamic elements are defined as elements which only start existing in the (shadow) DOM and that only become visible after certain events are triggered, e.g. clicking a button causes a modal window to pop up on screen.
    ///     </para>
    ///     <para>
    ///         Parents of dynamic elements need to implement the Initialise() and Dispose() members, in order to avoid null reference exceptions. These parents can be either page objects or other element objects.
    ///         <br/>
    ///         e.g. OnElementInitialisation() => Element.Initialise(); OnElementTermination() => Element.Dispose();
    ///     </para>
    /// </summary>
    protected ProtoElement(IPage page, string selector, bool initialiseElementHandle)
    {
        Page = page;
        ElementSelector = selector;
        ElementLocator = page.Locator(selector);

        if (initialiseElementHandle)
            Task.Run(async () => await Initialise()).Wait();
    }

    /// <summary>
    ///     Initialises an element object from an element handle object parent.
    /// </summary>
    protected ProtoElement(IElementHandle parentElement, string selector)
    {
        Page = Task.Run(async () => await parentElement.OwnerFrameAsync()).Result?.Page;
        ElementSelector = selector;
        ElementLocator = Page?.Locator(selector);
        ElementHandle = Task.Run(async () => await parentElement.QuerySelectorAsync(selector)).Result;
    }

    public IPage Page { get; init; }
    public string ElementSelector { get; init; }
    public ILocator ElementLocator { get; init; }
    public IElementHandle ElementHandle { get; private set; }

    public async Task Initialise()
    {
        await Page.WaitForSelectorAsync(ElementSelector);
        ElementHandle = await Page.QuerySelectorAsync(ElementSelector);
    }

    public async Task Dispose()
        => await ElementHandle.DisposeAsync();
}