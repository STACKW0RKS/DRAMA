using System.Collections.Generic;
using System.Threading.Tasks;
using DRAMA.Extensions;
using DRAMA.Models.TabularData;
using Microsoft.Playwright;

namespace DRAMA.Helpers.TabularData
{
    internal static class PlaywrightTableParser
    {
        // TODO: Find Elements Out Of Viewport
        // TODO: Add Cells InnerText
        // TODO: Add Cells Content
        // TODO: Create Interface For Table Parser To Enforce Populating The Entire Table Object
        // TODO: Complete This Method (Create ToTextTable() Extension Method)
        // TODO: Create NuGet Package

        /// <summary>
        ///     Parses an HTML table to a table of element handle objects which can be interacted with (e.g. read, click, evaluate).
        ///     <br/>
        ///     More information on the element handle API can be found at <a href="https://playwright.dev/dotnet/docs/next/api/class-elementhandle/"></a>.
        /// </summary>
        internal static async Task<Table<IElementHandle>> ParseTable(IElementHandle parent, TableParserSelectors selectors)
        {
            Table<IElementHandle> table = new();

            table = await ParseHeader(parent, table, selectors);
            table = await ParseRows(parent, table, selectors);
            table = await ParseFooter(parent, table, selectors);

            return table;
        }

        /// <summary>
        ///     Parses the header of an HTML table, and returns a table of element handles.
        ///     <br/>
        ///     Preferably, this table header parser should be called before both the table body parser and the table footer parser.
        /// </summary>
        private static async Task<Table<IElementHandle>> ParseHeader(IElementHandle parent, Table<IElementHandle> table, TableParserSelectors selectors)
        {
            // Query The Table Element's Parent For The Header Row Element
            IElementHandle headerRow = await parent.QuerySelectorAsync(selectors.HeaderRowSelector);

            // If No Header Element Is Found, Stop Parsing And Return The Output Table As Is
            if (headerRow is null) return table;

            // Query The Header Row Element For Cell Elements; Since A Header Element Exists, This List Should Never Be Empty
            IReadOnlyList<IElementHandle> headerCells = await headerRow.QuerySelectorAllAsync(selectors.HeaderCellSelector);

            // If The Table Does Not Have Any Columns Yet, Create Columns
            if (table.Columns.None())

                // For Each Cell Element Discovered...
                for (int columnIndex = 0; columnIndex < headerCells.Count; columnIndex++)
                {
                    // Get The Current Cell Element
                    IElementHandle headerCell = headerCells[columnIndex];

                    // Create A Parsed Column That Corresponds To The Current Cell Element; Parsed Columns Are One-Indexed
                    Column<IElementHandle> parsedColumn = new(columnIndex.IncrementBy(1), await headerCell.InnerTextAsync(), table);

                    // Attach The Current Parsed Column To The Output Table
                    table.AddColumn(parsedColumn);
                }

            // Create The Parsed Header Row, Detached From The Output Table
            // The Parsed Header Row Has An Index Of Zero; All Other Parsed Rows Are One-Indexed, Even If The Table Does Not Have A Header
            Row<IElementHandle> parsedHeaderRow = new(0, table);

            // For Each Cell Element Discovered...
            for (int columnIndex = 0; columnIndex < headerCells.Count; columnIndex++)
            {
                // Get The Current Cell Element
                IElementHandle headerCell = headerCells[columnIndex];

                // Create A Parsed Cell From The Current Cell Element
                Cell<IElementHandle> parsedHeaderCell = new(headerCell, await headerCell.InnerTextAsync(), table, parsedHeaderRow, table.Columns[columnIndex]);

                // Add The Parsed Cell To The Parsed Header Row
                parsedHeaderRow.AddCell(parsedHeaderCell);

                // Set The Parsed Cell As The Header Of The Table Column With The Same Horizontal Index
                table.Columns[columnIndex].SetHeader(parsedHeaderCell);
            }

            // Set The Parsed Header Row To Be The Output Table's Header
            table.SetHeader(parsedHeaderRow);

            // Output The Parsed Table, Which Now Contains (At Least) Columns And The Table Header
            return table;
        }

        /// <summary>
        ///     Parses the body of an HTML table, and returns a table of element handles.
        ///     <br/>
        ///     Preferably, this table body parser should be called after the table header parser and before the table footer parser.
        /// </summary>
        private static async Task<Table<IElementHandle>> ParseRows(IElementHandle parent, Table<IElementHandle> table, TableParserSelectors selectors)
        {
            // Query The Table Element's Parent For The Body Row Elements
            IReadOnlyList<IElementHandle> bodyRows = await parent.QuerySelectorAllAsync(selectors.BodyRowSelector);

            // If No Body Row Elements Are Found, Stop Parsing And Return The Output Table As Is
            if (bodyRows.None()) return table;

            // For Each Body Row Element Discovered...
            for (int bodyRowIndex = 0; bodyRowIndex < bodyRows.Count; bodyRowIndex++)
            {
                // Get The Current Row Element
                IElementHandle bodyRow = bodyRows[bodyRowIndex];

                // Query The Current Row Element For Cell Elements; Since A Row Element Exists, This List Should Never Be Empty
                IReadOnlyList<IElementHandle> bodyRowCells = await bodyRow.QuerySelectorAllAsync(selectors.BodyCellSelector);

                // If The Table Does Not Have Any Columns Yet, Create Columns
                if (table.Columns.None())

                    // For Each Cell Element Discovered...
                    for (int columnIndex = 0; columnIndex < bodyRowCells.Count; columnIndex++)
                    {
                        // Get The Current Cell Element
                        IElementHandle bodyRowCell = bodyRowCells[columnIndex];

                        // Create A Parsed Column That Corresponds To The Current Cell Element; Parsed Columns Are One-Indexed
                        Column<IElementHandle> parsedColumn = new(columnIndex.IncrementBy(1), await bodyRowCell.InnerTextAsync(), table);

                        // Attach The Current Parsed Column To The Output Table
                        table.AddColumn(parsedColumn);
                    }

                // Create A Parsed Body Row, Detached From The Output Table; Non-Header Rows Are One-Indexed, Including The Footer
                Row<IElementHandle> parsedBodyRow = new(bodyRowIndex.IncrementBy(1), table);

                // For Each Cell Element Discovered...
                for (int columnIndex = 0; columnIndex < bodyRowCells.Count; columnIndex++)
                {
                    // Get The Current Cell Element
                    IElementHandle bodyRowCell = bodyRowCells[columnIndex];

                    // Create A Parsed Cell From The Current Cell Element
                    Cell<IElementHandle> parsedBodyRowCell = new(bodyRowCell, await bodyRowCell.InnerTextAsync(), table, parsedBodyRow, table.Columns[columnIndex]);

                    // Add The Parsed Cell To The Parsed Body Row
                    parsedBodyRow.AddCell(parsedBodyRowCell);

                    // Add The Parsed Cell To The Table Column With The Same Horizontal Index
                    table.Columns[columnIndex].AddCell(parsedBodyRowCell);
                }

                // Attach The Parsed Body Row To The Output Table
                table.AddRow(parsedBodyRow);
            }

            // Output The Parsed Table, Which Now Contains (At Least) Columns And The Body Rows
            return table;
        }

        /// <summary>
        ///     Parses the footer of an HTML table, and returns a table of element handles.
        ///     <br/>
        ///     Preferably, this table footer parser should be called after both the table header parser and the table body parser.
        /// </summary>
        private static async Task<Table<IElementHandle>> ParseFooter(IElementHandle parent, Table<IElementHandle> table, TableParserSelectors selectors)
        {
            // Query The Table Element's Parent For The Footer Row Element
            IElementHandle footerRow = await parent.QuerySelectorAsync(selectors.FooterRowSelector);

            // If No Footer Element Is Found, Stop Parsing And Return The Output Table As Is
            if (footerRow is null) return table;

            // Query The Footer Row Element For Cell Elements; Since A Footer Element Exists, This List Should Never Be Empty
            IReadOnlyList<IElementHandle> footerCells = await footerRow.QuerySelectorAllAsync(selectors.FooterCellSelector);

            // If The Table Does Not Have Any Columns Yet, Create Columns
            if (table.Columns.None())

                // For Each Cell Element Discovered...
                for (int columnIndex = 0; columnIndex < footerCells.Count; columnIndex++)
                {
                    // Get The Current Cell Element
                    IElementHandle footerCell = footerCells[columnIndex];

                    // Create A Parsed Column That Corresponds To The Current Cell Element; Parsed Columns Are One-Indexed
                    Column<IElementHandle> parsedColumn = new(columnIndex.IncrementBy(1), await footerCell.InnerTextAsync(), table);

                    // Attach The Current Parsed Column To The Output Table
                    table.AddColumn(parsedColumn);
                }

            // Create The Parsed Footer Row, Detached From The Output Table
            Row<IElementHandle> parsedFooterRow = new(table.Rows.Count.IncrementBy(1), table);

            // For Each Cell Element Discovered...
            for (int columnIndex = 0; columnIndex < footerCells.Count; columnIndex++)
            {
                // Get The Current Cell Element
                IElementHandle footerCell = footerCells[columnIndex];

                // Create A Parsed Cell From The Current Cell Element
                Cell<IElementHandle> parsedFooterCell = new(footerCell, await footerCell.InnerTextAsync(), table, parsedFooterRow, table.Columns[columnIndex]);

                // Add The Parsed Cell To The Parsed Footer Row
                parsedFooterRow.AddCell(parsedFooterCell);

                // Add The Parsed Cell To The Table Column With The Same Horizontal Index
                table.Columns[columnIndex].AddCell(parsedFooterCell);
            }

            // Set The Parsed Footer Row To Be The Output Table's Footer
            table.SetFooter(parsedFooterRow);

            // Output The Parsed Table, Which Now Contains (At Least) Columns And The Table Footer
            return table;
        }
    }
}