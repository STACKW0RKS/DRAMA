using System.Collections.Generic;
using System.Linq;
using DRAMA.Models.TabularData;
using Microsoft.Playwright;

namespace DRAMA.Extensions
{
    public static class CellExtensions
    {
        // TODO: Write Unit Tests For These Methods (Needs A DOM With A Table)

        /// <summary>
        ///     Returns each element handle in the list of cells.
        /// </summary>
        public static IEnumerable<IElementHandle> Content(this List<Cell<IElementHandle>> cells)
            => cells.Select(cell => cell.Content);

        /// <summary>
        ///     Returns the inner text of each element handle in the list of cells.
        /// </summary>
        public static IEnumerable<string> InnerText(this List<Cell<IElementHandle>> cells)
            => cells.Select(cell => cell.Content.InnerTextAsync().Result);
    }
}