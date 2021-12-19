namespace DRAMA.Extensions;

public static class RowExtensions
{
    /// <summary>
    ///     Returns row with defined index. If row with defined index does not exist, returns null.
    /// </summary>
    public static Row<TContent> RowByIndex<TContent>(this List<Row<TContent>> rows, int index)
    {
        if (index < 1) throw new ArgumentOutOfRangeException(nameof(index), index, "Row Index Is Less Than Minimum Row Collection Count");
        if (index > rows.Count) throw new ArgumentOutOfRangeException(nameof(index), index, "Row Index Is Greater Than Current Row Collection Count");

        return rows.Any() ? rows.Single(row => row.Index.Equals(index)) : null;
    }
}
