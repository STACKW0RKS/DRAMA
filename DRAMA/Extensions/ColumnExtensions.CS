namespace DRAMA.Extensions;

public static class ColumnExtensions
{
    /// <summary>
    ///     Returns column with defined index. If column with defined index does not exist, returns null.
    /// </summary>
    public static Column<TContent> ColumnByIndex<TContent>(this List<Column<TContent>> columns, int index)
    {
        if (index < 1) throw new ArgumentOutOfRangeException(nameof(index), index, "Column Index Is Less Than Minimum Column Collection Count");
        if (index > columns.Count) throw new ArgumentOutOfRangeException(nameof(index), index, "Column Index Is Greater Than Current Column Collection Count");

        return columns.Any() ? columns.Single(column => column.Index.Equals(index)) : null;
    }

    /// <summary>
    ///     Returns column with defined name. If column with defined name does not exist, returns null.
    /// </summary>
    public static Column<TContent> ColumnByName<TContent>(this List<Column<TContent>> columns, string name)
        => columns.Any(column => column.Name.Equals(name)) ? columns.Single(column => column.Name.Equals(name)) : null;
}
