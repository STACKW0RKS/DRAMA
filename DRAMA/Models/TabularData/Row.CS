using System.Collections.Generic;
using System.Linq;

namespace DRAMA.Models.TabularData
{
    public class Row<TContent>
    {
        /// <summary>
        ///     Creates a table row with content of the specified type.
        ///     <br />
        ///     The collection of cells is already defined, and passed as a parameter to the constructor.
        /// </summary>
        public Row(int index, IEnumerable<Cell<TContent>> cells, Table<TContent> table)
        {
            Index = index;
            Cells = cells;
            Table = table;
        }

        /// <summary>
        ///     Creates a table row with content of the specified type.
        ///     <br />
        ///     The collection of cells is not fully defined, and the Cells property needs to be set post object construction.
        /// </summary>
        public Row(int index, Table<TContent> table)
        {
            Index = index;
            Cells = Enumerable.Empty<Cell<TContent>>();
            Table = table;
        }

        public int Index { get; init; }
        public IEnumerable<Cell<TContent>> Cells { get; private set; }
        public Table<TContent> Table { get; init; }

        /// <summary>
        ///     Sets the collection of cells in this row.
        /// </summary>
        public void SetCells(IEnumerable<Cell<TContent>> cells)
            => Cells = cells;

        /// <summary>
        ///     Adds a cell to the collection of cells in this row.
        /// </summary>
        public void AddCell(Cell<TContent> cell)
            => Cells = Cells.Append(cell);
    }
}