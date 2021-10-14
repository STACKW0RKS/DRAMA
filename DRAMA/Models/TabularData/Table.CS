using System.Collections.Generic;

namespace DRAMA.Models.TabularData
{
    public class Table<TContent>
    {
        public Table(Row<TContent> header, IEnumerable<Row<TContent>> rows, IEnumerable<Column<TContent>> columns)
        {
            Header = header;
            Rows = rows;
            Columns = columns;
        }

        public Row<TContent> Header { get; init; }
        public IEnumerable<Row<TContent>> Rows { get; init; }
        public IEnumerable<Column<TContent>> Columns { get; init; }
    }

    public class Row<TContent>
    {
        public Row(int index, IEnumerable<Cell<TContent>> cells, Table<TContent> table)
        {
            Index = index;
            Cells = cells;
            Table = table;
        }

        public int Index { get; init; }
        public IEnumerable<Cell<TContent>> Cells { get; init; }
        public Table<TContent> Table { get; init; }
    }

    public class Column<TContent>
    {
        public Column(int index, string name, IEnumerable<Cell<TContent>> cells, Table<TContent> table)
        {
            Index = index;
            Name = name;
            Cells = cells;
            Table = table;
        }

        public int Index { get; init; }
        public string Name { get; init; }
        public IEnumerable<Cell<TContent>> Cells { get; init; }
        public Table<TContent> Table { get; init; }
    }

    public class Cell<TContent>
    {
        public Cell(int rowIndex, int columnIndex, string columnName, TContent content, Table<TContent> table)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            ColumnName = columnName;
            Content = content;
            Table = table;
        }

        public int RowIndex { get; init; }
        public int ColumnIndex { get; init; }
        public string ColumnName { get; init; }
        public TContent Content { get; init; }
        public Table<TContent> Table { get; init; }
    }
}