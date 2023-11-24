namespace DRAMA.Models.TabularData;

/// <summary>
///     The model for a table row with content of the specified type.
/// </summary>
public class Row<TContent>
{
    /// <summary>
    ///     Constructs a table row with content of the specified type.
    ///     <br />
    ///     The collection of cells is not fully defined, and the Cells property needs to be set post object construction.
    /// </summary>
    public Row(int index, Table<TContent> table)
    {
        Index = index;
        Cells = new List<Cell<TContent>>();
        Table = table;
    }

    /// <summary>
    ///     Constructs a table row with content of the specified type.
    ///     <br />
    ///     The collection of cells is already defined, and passed as an argument to the constructor's parameters.
    /// </summary>
    public Row(int index, List<Cell<TContent>> cells, Table<TContent> table)
    {
        Index = index;
        Cells = cells;
        Table = table;
    }

    /// <summary>
    ///     The index of the row.
    ///     The header row has an index of zero. Body rows and the footer row are one-indexed.
    /// </summary>
    public int Index { get; init; }

    /// <summary>
    ///     The collection of cells in the row.
    /// </summary>
    public List<Cell<TContent>> Cells { get; private set; }

    /// <summary>
    ///     The parent table of the row.
    /// </summary>
    public Table<TContent> Table { get; init; }

    /// <summary>
    ///     Sets the collection of cells in this row.
    /// </summary>
    public void SetCells(List<Cell<TContent>> cells)
        => Cells = cells;

    /// <summary>
    ///     Adds a cell to the collection of cells in this row.
    /// </summary>
    public void AddCell(Cell<TContent> cell)
        => Cells.Add(cell);

    /// <summary>
    ///     Adds cells to the collection of cells in this row.
    /// </summary>
    public void AddCells(List<Cell<TContent>> cells)
        => Cells.AddRange(cells);
}
