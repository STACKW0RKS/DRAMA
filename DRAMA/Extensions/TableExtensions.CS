using System.Linq;
using System.Threading.Tasks;
using DRAMA.Models.TabularData;
using Microsoft.Playwright;

namespace DRAMA.Extensions
{
    public static class TableExtensions
    {
        // TODO: Describe The Statements In This Method, And Add Summary
        // TODO: Write Unit Tests For These Methods (Needs A DOM With A Table)

        public static async Task<Table<string>> ToTextTable(this Table<IElementHandle> table)
        {
            Table<string> textTable = new Table<string>();

            if (table.Columns.None()) return textTable;

            foreach (Column<IElementHandle> column in table.Columns)
                textTable.AddColumn(new Column<string>(column.Index, column.Name, textTable));

            if (table.Header is not null)
            {
                Row<string> headerRow = new(table.Header.Index, textTable);

                for (int columnIndex = 0; columnIndex < table.Header.Cells.Count; columnIndex++)
                {
                    Cell<IElementHandle> cell = table.Header.Cells[columnIndex];

                    headerRow.AddCell(new Cell<string>(await cell.Content.InnerHTMLAsync(), await cell.Content.InnerTextAsync(), textTable, textTable.Header, textTable.Columns[columnIndex]));

                    textTable.Columns[columnIndex].SetHeader(textTable.Header.Cells[columnIndex]);
                }

                textTable.SetHeader(headerRow);
            }

            if (table.Rows.Any())
            {
                for (int bodyRowIndex = 0; bodyRowIndex < table.Rows.Count; bodyRowIndex++)
                {
                    Row<string> bodyRow = new(table.Rows[bodyRowIndex].Index, textTable);

                    for (int columnIndex = 0; columnIndex < table.Rows[bodyRowIndex].Cells.Count; columnIndex++)
                    {
                        Cell<IElementHandle> cell = table.Rows[bodyRowIndex].Cells[columnIndex];
                        bodyRow.AddCell(new Cell<string>(await cell.Content.InnerHTMLAsync(), await cell.Content.InnerTextAsync(), textTable, bodyRow, textTable.Columns[columnIndex]));
                    }

                    textTable.AddRow(bodyRow);
                }
            }

            if (table.Footer is not null)
            {
                Row<string> footerRow = new(table.Footer.Index, textTable);

                for (int columnIndex = 0; columnIndex < table.Footer.Cells.Count; columnIndex++)
                {
                    Cell<IElementHandle> cell = table.Footer.Cells[columnIndex];

                    footerRow.AddCell(new Cell<string>(await cell.Content.InnerHTMLAsync(), await cell.Content.InnerTextAsync(), textTable, textTable.Footer, textTable.Columns[columnIndex]));
                }

                textTable.SetFooter(footerRow);
            }

            return textTable;
        }
    }
}