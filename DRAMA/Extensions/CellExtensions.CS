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
    ///     Returns cell with defined row index. If cell with defined row index does not exist, returns null.
    /// </summary>
    public static Cell<TContent> CellByRowIndex<TContent>(this List<Cell<TContent>> cells, int rowIndex)
    {
        if (cells.None()) return null;

        if (rowIndex < 0) throw new ArgumentOutOfRangeException(nameof(rowIndex), rowIndex, "Row Index Is Less Than Minimum Row Collection Count");

        if (rowIndex > (cells.RandomElement().Table.Rows.Any() ? cells.RandomElement().Table.Rows.Last().Index : cells.RandomElement().Table.Header.Index))
            throw new ArgumentOutOfRangeException(nameof(rowIndex), rowIndex, "Row Index Is Greater Than Current Row Collection Count");

        return cells.Single(cell => cell.Row.Index.Equals(rowIndex));
    }

    /// <summary>
    ///     Returns cell with defined column index. If cell with defined column index does not exist, returns null.
    /// </summary>
    public static Cell<TContent> CellByColumnIndex<TContent>(this List<Cell<TContent>> cells, int columnIndex)
    {
        if (cells.None()) return null;

        if (columnIndex < 0) throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, "Column Index Is Less Than Minimum Column Collection Count");

        if (columnIndex > cells.RandomElement().Table.Columns.Last().Index) throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, "Row Index Is Greater Than Current Row Collection Count");

        return cells.Single(cell => cell.Column.Index.Equals(columnIndex));
    }

    /// <summary>
    ///     Returns cell with defined index. If cell with defined index does not exist, returns null.
    /// </summary>
    public static Cell<TContent> CellByIndex<TContent>(this List<Cell<TContent>> cells, int rowIndex, int columnIndex)
    {
        if (cells.None()) return null;

        if (rowIndex < 0) throw new ArgumentOutOfRangeException(nameof(rowIndex), rowIndex, "Row Index Is Less Than Minimum Row Collection Count");

        if (rowIndex > (cells.RandomElement().Table.Rows.Any() ? cells.RandomElement().Table.Rows.Last().Index : cells.RandomElement().Table.Header.Index))
            throw new ArgumentOutOfRangeException(nameof(rowIndex), rowIndex, "Row Index Is Greater Than Current Row Collection Count");

        if (columnIndex < 0) throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, "Column Index Is Less Than Minimum Column Collection Count");

        if (columnIndex > cells.RandomElement().Table.Columns.Last().Index) throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, "Row Index Is Greater Than Current Row Collection Count");

        return cells.Single(cell => cell.Row.Index.Equals(rowIndex) && cell.Column.Index.Equals(columnIndex));
    }
}
