namespace DRAMA.Extensions.Playwright;

public static class ElementHandleExtensions
{
    /// <summary>
    ///     <para>
    ///         Parses a standard HTML table to a table of element handle objects which can be interacted with (e.g. read, click, evaluate).
    ///     </para>
    ///     <para>
    ///         If no selectors are specified, the defaults will be used. The default selectors are the following:
    ///         <br />
    ///         Table Container Selector: <c>"table"</c>
    ///         <br />
    ///         Header Row Selector: <c>"thead > tr"</c>
    ///         <br />
    ///         Header Cell Selector: <c>"th"</c>
    ///         <br />
    ///         Body Row Selector: <c>"tbody > tr"</c>
    ///         <br />
    ///         Body Cell Selector: <c>"th, td"</c>
    ///         <br />
    ///         Footer Row Selector: <c>"tfoot > tr"</c>
    ///         <br />
    ///         Footer Cell Selector: <c>"th, td"</c>
    ///     </para>
    /// </summary>
    public static async Task<Table<IElementHandle>> ParseHtmlTable(this IElementHandle? parent, TableParserSelectors? selectors = null)
        => await TableParser.ParseTable(parent, selectors ?? new TableParserSelectors
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
    ///     <para>
    ///         Parses an AG Grid element to a table of element handle objects which can be interacted with (e.g. read, click, evaluate).
    ///         <br />
    ///         The implementation of the grid can be either JavaScript, Angular, React, or Vue. More information can be found at <a href="https://www.ag-grid.com/"></a>.
    ///     </para>
    ///     <para>
    ///         If no selectors are specified, the defaults will be used. The default selectors are the following:
    ///         <br />
    ///         Table Container Selector: <c>"div.ag-root-wrapper"</c>
    ///         <br />
    ///         Header Row Selector: <c>"div.ag-header-container > div.ag-header-row"</c>
    ///         <br />
    ///         Header Cell Selector: <c>"div.ag-header-cell"</c>
    ///         <br />
    ///         Body Row Selector: <c>":nth-match(div.ag-center-cols-container, 1) > div.ag-row"</c>
    ///         <br />
    ///         Body Cell Selector: <c>"div.ag-cell"</c>
    ///         <br />
    ///         Footer Row Selector: <c>":nth-match(div.ag-center-cols-container, 2) > div.ag-row"</c>
    ///         <br />
    ///         Footer Cell Selector: <c>"div.ag-cell"</c>
    ///     </para>
    /// </summary>
    public static async Task<Table<IElementHandle>> ParseAgGrid(this IElementHandle? parent, TableParserSelectors? selectors = null)
        => await TableParser.ParseTable(parent, selectors ?? new TableParserSelectors
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
