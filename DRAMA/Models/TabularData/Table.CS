namespace DRAMA.Models.TabularData;

/// <summary>
///     The model for a table with content of the specified type.
/// </summary>
public class Table<TContent>
{
    /// <summary>
    ///     Constructs a table with content of the specified type.
    ///     <br />
    ///     The header, rows, footer, and columns are not fully defined, and the Header, Rows, and Columns properties need to be set post object construction.
    /// </summary>
    public Table()
    {
        Header = null;
        Rows = new List<Row<TContent>>();
        Footer = null;
        Columns = new List<Column<TContent>>();
    }

    /// <summary>
    ///     Constructs a table with content of the specified type.
    ///     <br />
    ///     The header, rows, footer, and columns are already defined, and passed as arguments to the constructor's parameters.
    /// </summary>
    public Table(Row<TContent> header, List<Row<TContent>> rows, Row<TContent> footer, List<Column<TContent>> columns)
    {
        Header = header;
        Rows = rows;
        Footer = footer;
        Columns = columns;
    }

    /// <summary>
    ///     The table's header row.
    /// </summary>
    public Row<TContent> Header { get; private set; }

    /// <summary>
    ///     The collection of rows in the table's body.
    /// </summary>
    public List<Row<TContent>> Rows { get; private set; }

    /// <summary>
    ///     The table's footer row.
    /// </summary>
    public Row<TContent> Footer { get; private set; }

    /// <summary>
    ///     The collection of columns in the table.
    /// </summary>
    public List<Column<TContent>> Columns { get; private set; }

    /// <summary>
    ///     Sets the header of this table.
    /// </summary>
    public void SetHeader(Row<TContent> header)
        => Header = header;

    /// <summary>
    ///     Sets the collection of rows in this table.
    /// </summary>
    public void SetRows(List<Row<TContent>> rows)
        => Rows = rows;

    /// <summary>
    ///     Adds a row to the collection of rows in this table.
    /// </summary>
    public void AddRow(Row<TContent> row)
        => Rows.Add(row);

    /// <summary>
    ///     Adds rows to the collection of rows in this table.
    /// </summary>
    public void AddRows(List<Row<TContent>> rows)
        => Rows.AddRange(rows);

    /// <summary>
    ///     Sets the header of this table.
    /// </summary>
    public void SetFooter(Row<TContent> footer)
        => Footer = footer;

    /// <summary>
    ///     Sets the collection of columns in this table.
    /// </summary>
    public void SetColumns(List<Column<TContent>> columns)
        => Columns = columns;

    /// <summary>
    ///     Adds a column to the collection of columns in this table.
    /// </summary>
    public void AddColumn(Column<TContent> column)
        => Columns.Add(column);

    /// <summary>
    ///     Adds columns to the collection of columns in this table.
    /// </summary>
    public void AddColumns(List<Column<TContent>> columns)
        => Columns.AddRange(columns);
}
