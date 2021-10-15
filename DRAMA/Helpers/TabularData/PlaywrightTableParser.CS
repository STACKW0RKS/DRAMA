using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using DRAMA.Models.TabularData;
using Microsoft.Playwright;

namespace DRAMA.Helpers.TabularData
{
    public static class PlaywrightTableParser
    {
        private static IElementHandle Table;
        private static string BodyCellSelector;
        private static string BodyRowSelector;
        private static string HeaderCellSelector;
        private static string HeaderRowSelector;

        private static async Task InitialisePlaywrightTableParser(PlaywrightTableParserOptions options)
        {
            Table = await options.TableContainer.QuerySelectorAsync(options.TableSelector);
            HeaderRowSelector = options.HeaderRowSelector;
            HeaderCellSelector = options.HeaderCellSelector;
            BodyRowSelector = options.BodyRowSelector;
            BodyCellSelector = options.BodyCellSelector;
        }

        public static async Task<Table<IElementHandle>> ParseTable(PlaywrightTableParserOptions options)
        {
            await InitialisePlaywrightTableParser(options);

            Table<IElementHandle> table = new();

            IElementHandle header = await Table.QuerySelectorAsync(HeaderRowSelector);
            ImmutableList<IElementHandle> headerCells = header is not null ? (await header.QuerySelectorAllAsync(HeaderCellSelector)).ToImmutableList() : ImmutableList<IElementHandle>.Empty;

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

            ImmutableList<IElementHandle> rows = (await Table.QuerySelectorAllAsync(BodyRowSelector)).ToImmutableList();

            if (rows.Any())
                for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++)
                {
                    IElementHandle row = rows[rowIndex];
                    ImmutableList<IElementHandle> rowCells = (await row.QuerySelectorAllAsync(BodyCellSelector)).ToImmutableList();
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
    }
}