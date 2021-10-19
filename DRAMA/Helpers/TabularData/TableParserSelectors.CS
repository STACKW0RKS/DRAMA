namespace DRAMA.Helpers.TabularData
{
    public class TableParserSelectors
    {
        /// <summary>
        ///     Defines the selectors that the Playwright table parser will query an HTML document for, and the element to which querying for selectors will be limited to.
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

        /// <summary>
        ///     The CSS selector of the table footer row element. Playwright pseudo-selectors can also be used.
        /// </summary>
        public string FooterRowSelector { get; init; }

        /// <summary>
        ///     The CSS selector of table footer cell elements. Playwright pseudo-selectors can also be used.
        /// </summary>
        public string FooterCellSelector { get; init; }
    }
}