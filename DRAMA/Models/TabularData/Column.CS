using System.Collections.Generic;

namespace DRAMA.Models.TabularData
{
    public class Column<TContent>
    {
        /// <summary>
        ///     Creates a table column with content of the specified type.
        ///     <br/>
        ///     The header and the collection of cells are not fully defined, and the Header and Cells properties need to be set post object construction.
        /// </summary>
        public Column(int index, string name, Table<TContent> table)
        {
            Index = index;
            Name = name;
            Header = null;
            Cells = new List<Cell<TContent>>();
            Table = table;
        }

        /// <summary>
        ///     Creates a table column with content of the specified type.
        ///     <br/>
        ///     The header and the collection of cells are already defined, and passed as arguments to the constructor's parameters.
        /// </summary>
        public Column(int index, string name, Cell<TContent> header, List<Cell<TContent>> cells, Table<TContent> table)
        {
            Index = index;
            Name = name;
            Header = header;
            Cells = cells;
            Table = table;
        }

        public int Index { get; init; }
        public string Name { get; init; }
        public Cell<TContent> Header { get; private set; }
        public List<Cell<TContent>> Cells { get; private set; }
        public Table<TContent> Table { get; init; }

        /// <summary>
        ///     Sets the header of this column.
        /// </summary>
        public void SetHeader(Cell<TContent> header)
            => Header = header;

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