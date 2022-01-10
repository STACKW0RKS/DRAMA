namespace DRAMA.Extensions;

public static class CellExtensions
{
    /// <summary>
    ///     Returns each element handle in the list of cells.
    /// </summary>
    public static IEnumerable<IElementHandle> Content(this List<Cell<IElementHandle>> cells)
        => cells.Select(cell => cell.Content);

    /// <summary>
    ///     Returns the inner text of each element handle in the list of cells.
    /// </summary>
    public static IEnumerable<string> InnerText(this List<Cell<IElementHandle>> cells)
        => cells.Select(cell => cell.Content.InnerTextAsync().GetAwaiter().GetResult());

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
    public static IEnumerable<Cell<TContent>> CellsByRowIndex<TContent>(this List<Cell<TContent>> cells, int rowIndex)
    {
        if (cells.None()) return Enumerable.Empty<Cell<TContent>>();

        ValidateRowIndex(cells, rowIndex);

        return cells.Where(cell => cell.Row.Index.Equals(rowIndex));
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
    public static IEnumerable<Cell<TContent>> CellsByColumnIndex<TContent>(this List<Cell<TContent>> cells, int columnIndex)
    {
        if (cells.None()) return Enumerable.Empty<Cell<TContent>>();

        ValidateColumnIndex(cells, columnIndex);

        return cells.Where(cell => cell.Column.Index.Equals(columnIndex));
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
        int minRowIndex = cells.RandomElement().Table.Header is not null
            ? 0 : cells.RandomElement().Table.Rows.Any() || cells.RandomElement().Table.Footer is not null
                ? 1 : throw new ArgumentOutOfRangeException(nameof(minRowIndex), "Table Does Not Contain Any Rows");

        if (rowIndex < minRowIndex) throw new ArgumentOutOfRangeException(nameof(rowIndex), rowIndex, "Provided Row Index Is Less Than Current Minimum Row Index");

        int maxRowIndex = (cells.RandomElement().Table.Header is not null ? 1 : 0)
                          + (cells.RandomElement().Table.Rows.Any() ? cells.RandomElement().Table.Rows.Last().Index : 0)
                          + (cells.RandomElement().Table.Footer is not null ? 1 : 0);

        if (rowIndex > maxRowIndex) throw new ArgumentOutOfRangeException(nameof(rowIndex), rowIndex, "Provided Row Index Is Greater Than Current Maximum Row Index");
    }

    /// <summary>
    ///     Validates whether the provided column index is within the range of valid column indices.
    ///     An exception is thrown if column index validation fails.
    /// </summary>
    private static void ValidateColumnIndex<TContent>(IReadOnlyCollection<Cell<TContent>> cells, int columnIndex)
    {
        int minColumnIndex = cells.RandomElement().Table.Columns.Any() ? 1 : throw new ArgumentOutOfRangeException(nameof(minColumnIndex), "Table Does Not Contain Any Column");

        if (columnIndex < minColumnIndex) throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, "Provided Column Index Is Less Than Current Minimum Column Index");

        int maxColumnIndex = cells.RandomElement().Table.Columns.Any()
            ? cells.RandomElement().Table.Columns.Last().Index
            : throw new ArgumentOutOfRangeException(nameof(minColumnIndex), "Table Does Not Contain Any Column");

        if (columnIndex > maxColumnIndex) throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, "Provided Column Index Is Greater Than Current Maximum Column Index");
    }
}
