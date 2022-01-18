namespace DRAMA.Extensions.TabularData;

public static class RowExtensions
{
    // TODO: Write Unit Tests For This Method

    /// <summary>
    ///     Returns the row with the defined index. If the row with the defined index does not exist, returns null.
    /// </summary>
    public static Row<TContent>? RowByIndex<TContent>(this List<Row<TContent>> rows, int index)
    {
        if (rows.None()) return null;

        if (index < rows.Select(row => row.Index).Min()) throw new ArgumentOutOfRangeException(nameof(index), index, "Provided Row Index Is Less Than Minimum Row Index");
        if (index > rows.Select(row => row.Index).Max()) throw new ArgumentOutOfRangeException(nameof(index), index, "Provided Row Index Is Greater Than Current Maximum Row Index");

        return rows.SingleOrDefault(row => row.Index.Equals(index));
    }
}
