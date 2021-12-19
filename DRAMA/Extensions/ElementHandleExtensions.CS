namespace DRAMA.Extensions;

public static class ElementHandleExtensions
{
    /// <summary>
    ///     Parses a standard HTML table to a table of element handle objects which can be interacted with (e.g. read, click, evaluate).
    /// </summary>
    public static async Task<Table<IElementHandle>> ParseHtmlTable(this IElementHandle parent, TableParserSelectors selectors = null)
        => await PlaywrightTableParser.ParseTable(parent, selectors ?? new TableParserSelectors
        (
            "table",
            "thead > tr",
            "th",
            "tbody > tr",
            "th, td",
            "tfoot > tr",
            "th, td"
        ));

    /// <summary>
    ///     Parses an AG Grid element to a table of element handle objects which can be interacted with (e.g. read, click, evaluate).
    ///     <br />
    ///     The implementation of the grid can be either JavaScript, Angular, React, or Vue. More information can be found at <a href="https://www.ag-grid.com/"></a>.
    /// </summary>
    public static async Task<Table<IElementHandle>> ParseAgGrid(this IElementHandle parent, TableParserSelectors selectors = null)
        => await PlaywrightTableParser.ParseTable(parent, selectors ?? new TableParserSelectors
        (
            "div.ag-root-wrapper",
            "div.ag-header-container > div.ag-header-row",
            "div.ag-header-cell",
            ":nth-match(div.ag-center-cols-container, 1) > div.ag-row",
            "div.ag-cell",
            ":nth-match(div.ag-center-cols-container, 2) > div.ag-row",
            "div.ag-cell"
        ));
}
