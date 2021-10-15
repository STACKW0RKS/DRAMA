using System.Collections.Generic;
using System.Linq;

namespace DRAMA.Models.TabularData
{
    public class Column<TContent>
    {
        /// <summary>
        ///     Creates a table column with content of the specified type.
        ///     <br />
        ///     The collection of cells is already defined, and passed as a parameter to the constructor.
        /// </summary>
        public Column(int index, string name, IEnumerable<Cell<TContent>> cells, Table<TContent> table)
        {
            Index = index;
            Name = name;
            Cells = cells;
            Table = table;
        }

        /// <summary>
        ///     Creates a table column with content of the specified type.
        ///     <br />
        ///     The collection of cells is not fully defined, and the Cells property needs to be set post object construction.
        /// </summary>
        public Column(int index, string name, Table<TContent> table)
        {
            Index = index;
            Name = name;
            Cells = Enumerable.Empty<Cell<TContent>>();
            Table = table;
        }

        public int Index { get; init; }
        public string Name { get; init; }
        public IEnumerable<Cell<TContent>> Cells { get; private set; }
        public Table<TContent> Table { get; init; }

        /// <summary>
        ///     Sets the collection of cells in this column.
        /// </summary>
        public void SetCells(IEnumerable<Cell<TContent>> cells)
            => Cells = cells;

        /// <summary>
        ///     Adds a cell to the collection of cells in this column.
        /// </summary>
        public void AddCell(Cell<TContent> cell)
            => Cells = Cells.Append(cell);
    }
}