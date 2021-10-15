using System.Collections.Generic;

namespace DRAMA.Models.TabularData
{
    public class Column<TContent>
    {
        /// <summary>
        ///     Creates a table column with content of the specified type.
        ///     <br/>
        ///     The collection of cells is already defined, and passed as a parameter to the constructor.
        /// </summary>
        public Column(int index, string name, List<Cell<TContent>> cells, Table<TContent> table)
        {
            Index = index;
            Name = name;
            Cells = cells;
            Table = table;
        }

        /// <summary>
        ///     Creates a table column with content of the specified type.
        ///     <br/>
        ///     The collection of cells is not fully defined, and the Cells property needs to be set post object construction.
        /// </summary>
        public Column(int index, string name, Table<TContent> table)
        {
            Index = index;
            Name = name;
            Cells = new List<Cell<TContent>>();
            Table = table;
        }

        public int Index { get; init; }
        public string Name { get; init; }
        public List<Cell<TContent>> Cells { get; private set; }
        public Table<TContent> Table { get; init; }

        /// <summary>
        ///     Sets the collection of cells in this column.
        /// </summary>
        public void SetCells(List<Cell<TContent>> cells)
            => Cells = cells;

        /// <summary>
        ///     Adds a cell to the collection of cells in this column.
        /// </summary>
        public void AddCell(Cell<TContent> cell)
            => Cells.Add(cell);

        /// <summary>
        ///     Adds cells to the collection of cells in this column.
        /// </summary>
        public void AddCells(List<Cell<TContent>> cells)
            => Cells.AddRange(cells);
    }
}