namespace DRAMA.Models.TabularData;

/// <summary>
///     The model for a table column with content of the specified type.
/// </summary>
public class Column<TContent>
{
    /// <summary>
    ///     Constructs a table column with content of the specified type.
    ///     <br />
    ///     The header and the collection of cells are not fully defined, and the Header and Cells properties need to be set post object construction.
    /// </summary>
    public Column(int index, string name, Table<TContent> table)
    {
        Index = index;
        Name = name;
        Header = null;
        Cells = new List<Cell<TContent>>();
        Table = table;
    }

    /// <summary>
    ///     Constructs a table column with content of the specified type.
    ///     <br />
    ///     The header and the collection of cells are already defined, and passed as arguments to the constructor's parameters.
    /// </summary>
    public Column(int index, string name, Cell<TContent> header, List<Cell<TContent>> cells, Table<TContent> table)
    {
        Index = index;
        Name = name;
        Header = header;
        Cells = cells;
        Table = table;
    }

    /// <summary>
    ///     The index of the column. Columns are one-indexed.
    /// </summary>
    public int Index { get; init; }

    /// <summary>
    ///     The name of the column.
    ///     In the case of most table parsers, this will be the text of the header cell.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    ///     The header cell of the column.
    /// </summary>
    public Cell<TContent> Header { get; private set; }

    /// <summary>
    ///     The collection of cells in the column.
    ///     Does not include the header cell.
    /// </summary>
    public List<Cell<TContent>> Cells { get; private set; }

    /// <summary>
    ///     The parent table of the column.
    /// </summary>
    public Table<TContent> Table { get; init; }

    /// <summary>
    ///     Sets the header of this column.
    /// </summary>
    public void SetHeader(Cell<TContent> header)
        => Header = header;

    /// <summary>
    ///     Sets the collection of cells in this column.
    /// </summary>
    public void SetCells(List<Cell<TContent>> cells)
        => Cells = cells;

    /// <summary>
    ///     Adds a cell to the collection of cells in this column.
    /// </summary>
    public void AddCell(Cell<TContent> cell)
        => Cells.Add(cell);

    /// <summary>
    ///     Adds cells to the collection of cells in this column.
    /// </summary>
    public void AddCells(List<Cell<TContent>> cells)
        => Cells.AddRange(cells);
}
