using System.Threading.Tasks;
using DRAMA.Helpers.TabularData;
using DRAMA.Models.TabularData;
using Microsoft.Playwright;

namespace DRAMA.Extensions
{
    public static class ElementHandleExtensions
    {
        // TODO: Populate These Methods With Summaries
        // TODO: Write Unit Tests For These Methods

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

        // https://www.ag-grid.com/
        // JS, Angular, React, Vue
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
}