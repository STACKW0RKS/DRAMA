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

        internal static async Task<Table<IElementHandle>> ParseTable(IElementHandle parent, TableParserSelectors selectors)
        {
            Table<IElementHandle> table = new Table<IElementHandle>();

            table = await ParseHeader(parent, table, selectors);
            table = await ParseRows(parent, table, selectors);
            table = await ParseFooter(parent, table, selectors);

            return table;
        }

        private static async Task<Table<IElementHandle>> ParseHeader(IElementHandle parent, Table<IElementHandle> table, TableParserSelectors selectors)
        {
            // Query The Table Element's Parent For The Header Row Element
            IElementHandle headerRow = await parent.QuerySelectorAsync(selectors.HeaderRowSelector);

            // If No Header Element Is Found, Stop Parsing And Return The Output Table As Is
            if (headerRow is null) return table;

            // Query The Header Row Element For Cell Elements; Since A Header Element Exists, This List Should Never Be Empty
            IReadOnlyList<IElementHandle> headerCells = await headerRow.QuerySelectorAllAsync(selectors.HeaderCellSelector);

            // Create The Parsed Header Row, Detached From The Output Table
            // The Parsed Header Row Has An Index Of Zero; All Other Parsed Rows Are One-Indexed, Even If The Table Does Not Have A Header
            Row<IElementHandle> parsedHeaderRow = new(0, table);

            // For Each Cell Element Discovered...
            for (int columnIndex = 0; columnIndex < headerCells.Count; columnIndex++)
            {
                // Get The Current Cell Element
                IElementHandle headerCell = headerCells[columnIndex];

                // Create A Parsed Column That Corresponds To The Current Cell Element; Parsed Columns Are One-Indexed
                Column<IElementHandle> parsedColumn = new(columnIndex.IncrementBy(1), await headerCell.InnerTextAsync(), table);

                // Create A Parsed Cell From The Current Cell Element
                Cell<IElementHandle> parsedHeaderCell = new(headerCell, await headerCell.InnerTextAsync(), table, parsedHeaderRow, parsedColumn);

                // Add The Parsed Cell To The Parsed Header Row
                parsedHeaderRow.AddCell(parsedHeaderCell);

                // Set The Parsed Cell As The Current Parsed Column's Header
                parsedColumn.SetHeader(parsedHeaderCell);

                // Attach The Current Parsed Column To The Output Table
                table.AddColumn(parsedColumn);
            }

            // Set The Parsed Header Row To Be The Output Table's Header
            table.SetHeader(parsedHeaderRow);

            // Return The Partially Parsed Table, Which Now Contains Columns And The Header
            return table;
        }

        private static async Task<Table<IElementHandle>> ParseRows(IElementHandle parent, Table<IElementHandle> table, TableParserSelectors selectors)
        {

        }

        private static async Task<Table<IElementHandle>> ParseFooter(IElementHandle parent, Table<IElementHandle> table, TableParserSelectors selectors)
        {

        }











        /*
        internal static async Task<Table<IElementHandle>> ParseTable(IElementHandle parent, TableParserSelectors selectors)
        {
            Table<IElementHandle> table = new();

            IElementHandle header = await parent.QuerySelectorAsync(selectors.HeaderRowSelector);
            ImmutableList<IElementHandle> headerCells = header is not null ? (await header.QuerySelectorAllAsync(selectors.HeaderCellSelector)).ToImmutableList() : ImmutableList<IElementHandle>.Empty;

            if (headerCells.Any())
                for (int columnIndex = 0; columnIndex < headerCells.Count; columnIndex++)
                {
                    IElementHandle headerCell = headerCells[columnIndex];
                    Cell<IElementHandle> parsedHeaderCell = new(0, columnIndex + 1, await headerCell.InnerTextAsync(), headerCell, table);
                    table.Header.AddCell(parsedHeaderCell);
                    Column<IElementHandle> parsedColumn = new(columnIndex + 1, await headerCell.InnerTextAsync(), table);
                    table.AddColumn(parsedColumn);
                    table.Columns[columnIndex].AddCell(parsedHeaderCell);
                }

            ImmutableList<IElementHandle> rows = (await parent.QuerySelectorAllAsync(selectors.BodyRowSelector)).ToImmutableList();

            if (rows.None()) return table;

            for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++)
            {
                IElementHandle row = rows[rowIndex];
                ImmutableList<IElementHandle> rowCells = (await row.QuerySelectorAllAsync(selectors.BodyCellSelector)).ToImmutableList();
                Row<IElementHandle> parsedRow = new(rowIndex + 1, table);
                table.AddRow(parsedRow);

                for (int columnIndex = 0; columnIndex < rowCells.Count; columnIndex++)
                {
                    IElementHandle rowCell = rowCells[columnIndex];
                    Cell<IElementHandle> parsedRowCell = new(rowIndex + 1, columnIndex + 1, table.Columns[columnIndex].Name, rowCell, table);
                    table.Rows[rowIndex].AddCell(parsedRowCell);
                    table.Columns[columnIndex].AddCell(parsedRowCell);
                }
            }

            return table;
        }
        */

        /*
        public static async Task<Table<string>> ParseTableToText(IElementHandle parent, PlaywrightTableParserOptions options)
        {
            Table<IElementHandle> elementTable = await ParseTable(parent, options);
            Table<string> textTable = new();

            // TODO: Complete This Method (Create ToTextTable() Extension Method)
            // TODO: Write Member Descriptions
            // TODO: Create Unit Tests

            // TODO: Rename TRIAL To AUDITION
            // TODO: Create NuGet Package

            return default;
        }
        */
    }
}