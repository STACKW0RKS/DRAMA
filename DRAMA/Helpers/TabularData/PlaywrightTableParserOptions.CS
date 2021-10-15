using Microsoft.Playwright;

namespace DRAMA.Helpers.TabularData
{
    public class PlaywrightTableParserOptions
    {
        /// <summary>
        ///     Defines the selectors that the Playwright table parser will query an HTML document for, and the element to which querying for selectors will be limited to.
        /// </summary>
        public PlaywrightTableParserOptions(IElementHandle tableContainer, string tableSelector, string headerRowSelector, string headerCellSelector, string bodyRowSelector, string bodyCellSelector)
        {
            TableContainer = tableContainer;
            TableSelector = tableSelector;
            HeaderRowSelector = headerRowSelector;
            HeaderCellSelector = headerCellSelector;
            BodyRowSelector = bodyRowSelector;
            BodyCellSelector = bodyCellSelector;
        }

        /// <summary>
        ///     This element can either be the direct or indirect parent of the table, or the table element itself.
        /// </summary>
        public IElementHandle TableContainer { get; init; }

        /// <summary>
        ///     The CSS selector of the table element. Playwright pseudo-selectors can also be used.
        /// </summary>
        public string TableSelector { get; init; }

        /// <summary>
        ///     The CSS selector of the table header row element. Playwright pseudo-selectors can also be used.
        /// </summary>
        public string HeaderRowSelector { get; init; }

        /// <summary>
        ///     The CSS selector of table header cell elements. Playwright pseudo-selectors can also be used.
        /// </summary>
        public string HeaderCellSelector { get; init; }

        /// <summary>
        ///     The CSS selector of table body row elements. Playwright pseudo-selectors can also be used.
        /// </summary>
        public string BodyRowSelector { get; init; }

        /// <summary>
        ///     The CSS selector of table body cell elements. Playwright pseudo-selectors can also be used.
        /// </summary>
        public string BodyCellSelector { get; init; }
    }
}