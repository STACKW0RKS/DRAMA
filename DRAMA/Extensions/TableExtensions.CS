namespace DRAMA.Extensions;

public static class TableExtensions
{
    // TODO: Write Unit Tests For These Methods

    /// <summary>
    ///     Returns the collection of cells in the table.
    /// </summary>
    public static List<Cell<TContent>> Cells<TContent>(this Table<TContent> table)
    {
        List<Cell<TContent>> cells = new();

        if (table.Header is not null) cells.AddRange(table.Header.Cells);

        if (table.Rows.Any())
            foreach (Row<TContent> row in table.Rows)
                cells.AddRange(row.Cells);

        if (table.Footer is not null) cells.AddRange(table.Footer.Cells);

        return cells;
    }

    /// <summary>
    ///     Returns the collection of all rows in the table, including any rows in the table's header and footer, if these exist.
    /// </summary>
    public static List<Row<TContent>> HeaderBodyFooterRows<TContent>(this Table<TContent> table)
    {
        List<Row<TContent>> rows = new();

        if (table.Header is not null) rows.Add(table.Header);
        if (table.Rows.Any()) rows.AddRange(table.Rows);
        if (table.Footer is not null) rows.Add(table.Footer);

        return rows;
    }

    /// <summary>
    ///     Parses a table of element handles to a table of text values.
    /// </summary>
    public static async Task<Table<string>> ToTextTable(this Table<IElementHandle> table)
    {
        // Create A New Text Table
        Table<string> textTable = new();

        // If The Source Table Contains No Columns, Stop Parsing And Return The Output Table As Is
        // Each Table Parser (Header/Body/Footer) Can Create Columns, So If Columns Still Do Not Exist After Full Table Parsing Then Something Has Gone Wrong
        if (table.Columns.None()) return textTable;

        // For Each Column In The Source Table...
        foreach (Column<IElementHandle> column in table.Columns)

            // Create A New Column In The Output Table
            textTable.AddColumn(new Column<string>(column.Index, column.Name, textTable));

        // If The Source Table Has A Header...
        if (table.Header is not null)
        {
            // Create The Parsed Header Row, Detached From The Output Table
            Row<string> headerRow = new(table.Header.Index, textTable);

            // For Each Cell In The Source Table Header...
            for (int columnIndex = 0; columnIndex < table.Header.Cells.Count; columnIndex++)
            {
                // Get The Current Parsed Cell
                Cell<IElementHandle> cell = table.Header.Cells[columnIndex];

                // Add A Parsed Cell To The Parsed Header Row Of The Output Table
                headerRow.AddCell(new Cell<string>(await cell.Content.InnerHTMLAsync(), await cell.Content.InnerTextAsync(), textTable, textTable.Header, textTable.Columns[columnIndex]));

                // Set The Parsed Cell As The Header Of The Output Table Column With The Same Horizontal Index
                textTable.Columns[columnIndex].SetHeader(textTable.Header.Cells[columnIndex]);
            }

            // Set The Parsed Header Row To Be The Output Table's Header
            textTable.SetHeader(headerRow);
        }

        // If The Source Table Contains Any Body Rows...
        if (table.Rows.Any())

            // For Each Body Row In The Source Table...
            for (int bodyRowIndex = 0; bodyRowIndex < table.Rows.Count; bodyRowIndex++)
            {
                // Create A New Body Row With Text Content In The Output Table
                Row<string> bodyRow = new(table.Rows[bodyRowIndex].Index, textTable);

                // For Each Cell In The Body Row...
                for (int columnIndex = 0; columnIndex < table.Rows[bodyRowIndex].Cells.Count; columnIndex++)
                {
                    // Get The Current Parsed Cell
                    Cell<IElementHandle> cell = table.Rows[bodyRowIndex].Cells[columnIndex];

                    // Add A Parsed Cell To The Parsed Body Row Of The Output Table
                    bodyRow.AddCell(new Cell<string>(await cell.Content.InnerHTMLAsync(), await cell.Content.InnerTextAsync(), textTable, bodyRow, textTable.Columns[columnIndex]));
                }

                // Attach The Parsed Body Row To The Output Table
                textTable.AddRow(bodyRow);
            }

        // If The Source Table Has A Footer...
        if (table.Footer is not null)
        {
            // Create The Parsed Footer Row, Detached From The Output Table
            Row<string> footerRow = new(table.Footer.Index, textTable);

            // For Each Cell In The Source Table Footer...
            for (int columnIndex = 0; columnIndex < table.Footer.Cells.Count; columnIndex++)
            {
                // Get The Current Parsed Cell
                Cell<IElementHandle> cell = table.Footer.Cells[columnIndex];

                // Add A Parsed Cell To The Parsed Footer Row Of The Output Table
                footerRow.AddCell(new Cell<string>(await cell.Content.InnerHTMLAsync(), await cell.Content.InnerTextAsync(), textTable, textTable.Footer, textTable.Columns[columnIndex]));
            }

            // Set The Parsed Footer Row To Be The Output Table's Header
            textTable.SetFooter(footerRow);
        }

        // Output The Fully Parsed Table
        return textTable;
    }
}
