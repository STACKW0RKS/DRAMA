namespace DRAMA.Extensions;

public static class CellExtensions
{
    // TODO: Write Unit Tests For These Methods

    /// <summary>
    ///     Returns each element handle in the list of cells.
    /// </summary>
    public static List<IElementHandle> Content(this List<Cell<IElementHandle>> cells)
        => cells.Select(cell => cell.Content).ToList();

    /// <summary>
    ///     Returns the inner text of each element handle in the list of cells.
    /// </summary>
    public static List<string> InnerText(this List<Cell<IElementHandle>> cells)
        => cells.Select(cell => cell.Text).ToList();

    /// <summary>
    ///     Returns the cell with the defined row index. If the cell with the defined row index does not exist, returns null.
    /// </summary>
    public static Cell<TContent> CellByRowIndex<TContent>(this List<Cell<TContent>> cells, int rowIndex)
    {
        if (cells.None()) return null;

        ValidateRowIndex(cells, rowIndex);

        return cells.SingleOrDefault(cell => cell.Row.Index.Equals(rowIndex));
    }

    /// <summary>
    ///     Returns the cells with the defined row index. If no cells with the defined row index exist, returns an empty collection.
    /// </summary>
    public static List<Cell<TContent>> CellsByRowIndex<TContent>(this List<Cell<TContent>> cells, int rowIndex)
    {
        if (cells.None()) return new List<Cell<TContent>>();

        ValidateRowIndex(cells, rowIndex);

        return cells.Where(cell => cell.Row.Index.Equals(rowIndex)).ToList();
    }

    /// <summary>
    ///     Returns the cell with the defined column index. If the cell with the defined column index does not exist, returns null.
    /// </summary>
    public static Cell<TContent> CellByColumnIndex<TContent>(this List<Cell<TContent>> cells, int columnIndex)
    {
        if (cells.None()) return null;

        ValidateColumnIndex(cells, columnIndex);

        return cells.SingleOrDefault(cell => cell.Column.Index.Equals(columnIndex));
    }

    /// <summary>
    ///     Returns the cells with the defined column index. If no cells with the defined column index exist, returns an empty collection.
    /// </summary>
    public static List<Cell<TContent>> CellsByColumnIndex<TContent>(this List<Cell<TContent>> cells, int columnIndex)
    {
        if (cells.None()) return new List<Cell<TContent>>();

        ValidateColumnIndex(cells, columnIndex);

        return cells.Where(cell => cell.Column.Index.Equals(columnIndex)).ToList();
    }

    /// <summary>
    ///     Returns the cell with the defined index. If the cell with the defined index does not exist, returns null.
    /// </summary>
    public static Cell<TContent> CellByIndex<TContent>(this List<Cell<TContent>> cells, int rowIndex, int columnIndex)
    {
        if (cells.None()) return null;

        ValidateRowIndex(cells, rowIndex);
        ValidateColumnIndex(cells, columnIndex);

        return cells.SingleOrDefault(cell => cell.Row.Index.Equals(rowIndex) && cell.Column.Index.Equals(columnIndex));
    }

    /// <summary>
    ///     Validates whether the provided row index is within the range of valid row indices.
    ///     An exception is thrown if row index validation fails.
    /// </summary>
    private static void ValidateRowIndex<TContent>(IReadOnlyCollection<Cell<TContent>> cells, int rowIndex)
    {
        if (cells.RandomElement().Table.HeaderBodyFooterRows().None())
            throw new ArgumentOutOfRangeException(nameof(rowIndex), rowIndex, "Table Does Not Contain Any Rows");

        if (rowIndex < cells.RandomElement().Table.HeaderBodyFooterRows().Select(row => row.Index).Min())
            throw new ArgumentOutOfRangeException(nameof(rowIndex), rowIndex, "Provided Row Index Is Less Than Current Minimum Row Index");

        if (rowIndex > cells.RandomElement().Table.HeaderBodyFooterRows().Select(row => row.Index).Max())
            throw new ArgumentOutOfRangeException(nameof(rowIndex), rowIndex, "Provided Row Index Is Greater Than Current Maximum Row Index");
    }

    /// <summary>
    ///     Validates whether the provided column index is within the range of valid column indices.
    ///     An exception is thrown if column index validation fails.
    /// </summary>
    private static void ValidateColumnIndex<TContent>(IReadOnlyCollection<Cell<TContent>> cells, int columnIndex)
    {
        if (cells.RandomElement().Table.Columns.None())
            throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, "Table Does Not Contain Any Columns");

        if (columnIndex < cells.RandomElement().Table.Columns.Select(column => column.Index).Min())
            throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, "Provided Column Index Is Less Than Current Minimum Column Index");

        if (columnIndex > cells.RandomElement().Table.Columns.Select(column => column.Index).Max())
            throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, "Provided Column Index Is Greater Than Current Maximum Column Index");
    }
}
