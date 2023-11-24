namespace DRAMA.Models.TabularData;

/// <summary>
///     The model for a table cell with content of the specified type.
/// </summary>
public class Cell<TContent>
{
    /// <summary>
    ///     Constructs a table cell with content of the specified type.
    /// </summary>
    public Cell(TContent content, string text, Table<TContent> table, Row<TContent> row, Column<TContent> column)
    {
        Content = content;
        Text = text;
        Table = table;
        Row = row;
        Column = column;
    }

    /// <summary>
    ///     The content of the cell.
    /// </summary>
    public TContent Content { get; init; }

    /// <summary>
    ///     The text of the cell.
    /// </summary>
    public string Text { get; init; }

    /// <summary>
    ///     The parent table of the cell.
    /// </summary>
    public Table<TContent> Table { get; init; }

    /// <summary>
    ///     The parent row of the cell.
    /// </summary>
    public Row<TContent> Row { get; init; }

    /// <summary>
    ///     The parent column of the cell.
    /// </summary>
    public Column<TContent> Column { get; init; }
}
