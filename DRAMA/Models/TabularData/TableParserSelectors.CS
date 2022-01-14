namespace DRAMA.Models.TabularData;

/// <summary>
///     The model for the set of selectors that the table parser will use to read one or more tables from an HTML document.
/// </summary>
public class TableParserSelectors
{
    /// <summary>
    ///     Defines the selectors that the Playwright table parser will query an HTML document for, and the containing element to which querying for selectors will be limited to.
    /// </summary>
    public TableParserSelectors
    (
        string tableSelector,
        string headerRowSelector,
        string headerCellSelector,
        string bodyRowSelector,
        string bodyCellSelector,
        string footerRowSelector,
        string footerCellSelector
    )
    {
        TableSelector = tableSelector;
        HeaderRowSelector = headerRowSelector;
        HeaderCellSelector = headerCellSelector;
        BodyRowSelector = bodyRowSelector;
        BodyCellSelector = bodyCellSelector;
        FooterRowSelector = footerRowSelector;
        FooterCellSelector = footerCellSelector;
    }

    /// <summary>
    ///     The selector of the table element.
    ///     More information on element selectors can be found at <a href="https://playwright.dev/dotnet/docs/next/selectors"></a>.
    /// </summary>
    public string TableSelector { get; init; }

    /// <summary>
    ///     The selector of the table header row element.
    ///     More information on element selectors can be found at <a href="https://playwright.dev/dotnet/docs/next/selectors"></a>.
    /// </summary>
    public string HeaderRowSelector { get; init; }

    /// <summary>
    ///     The selector of table header cell elements.
    ///     More information on element selectors can be found at <a href="https://playwright.dev/dotnet/docs/next/selectors"></a>.
    /// </summary>
    public string HeaderCellSelector { get; init; }

    /// <summary>
    ///     The selector of table body row elements.
    ///     More information on element selectors can be found at <a href="https://playwright.dev/dotnet/docs/next/selectors"></a>.
    /// </summary>
    public string BodyRowSelector { get; init; }

    /// <summary>
    ///     The selector of table body cell elements.
    ///     More information on element selectors can be found at <a href="https://playwright.dev/dotnet/docs/next/selectors"></a>.
    /// </summary>
    public string BodyCellSelector { get; init; }

    /// <summary>
    ///     The selector of the table footer row element.
    ///     More information on element selectors can be found at <a href="https://playwright.dev/dotnet/docs/next/selectors"></a>.
    /// </summary>
    public string FooterRowSelector { get; init; }

    /// <summary>
    ///     The selector of table footer cell elements.
    ///     More information on element selectors can be found at <a href="https://playwright.dev/dotnet/docs/next/selectors"></a>.
    /// </summary>
    public string FooterCellSelector { get; init; }
}
