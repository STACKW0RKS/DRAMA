namespace DRAMA.Models.TabularData;

public class Cell<TContent>
{
    /// <summary>
    ///     Creates a table cell with content of the specified type.
    /// </summary>
    public Cell(TContent content, string text, Table<TContent> table, Row<TContent> row, Column<TContent> column)
    {
        Content = content;
        Text = text;
        Table = table;
        Row = row;
        Column = column;
    }

    public TContent Content { get; init; }
    public string Text { get; init; }
    public Table<TContent> Table { get; init; }
    public Row<TContent> Row { get; init; }
    public Column<TContent> Column { get; init; }
}
