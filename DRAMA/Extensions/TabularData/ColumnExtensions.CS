namespace DRAMA.Extensions.TabularData;

public static class ColumnExtensions
{
    // TODO: Write Unit Tests For These Methods

    /// <summary>
    ///     Returns the column with the defined index. If the column with the defined index does not exist, returns null.
    /// </summary>
    public static Column<TContent>? ColumnByIndex<TContent>(this List<Column<TContent>> columns, int index)
    {
        if (columns.None()) return null;

        if (index < columns.Select(column => column.Index).Min()) throw new ArgumentOutOfRangeException(nameof(index), index, "Provided Column Index Is Less Than Minimum Column Index");
        if (index > columns.Select(column => column.Index).Max()) throw new ArgumentOutOfRangeException(nameof(index), index, "Provided Column Index Is Greater Than Current Maximum Column Index");

        return columns.SingleOrDefault(column => column.Index.Equals(index));
    }

    /// <summary>
    ///     Returns the column with the defined name. If the column with the defined name does not exist, returns null.
    /// </summary>
    public static Column<TContent>? ColumnByName<TContent>(this List<Column<TContent>> columns, string name)
        => columns.None() ? null : columns.SingleOrDefault(column => column.Name.Equals(name));
}
