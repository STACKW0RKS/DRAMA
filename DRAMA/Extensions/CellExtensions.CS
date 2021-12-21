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
}
