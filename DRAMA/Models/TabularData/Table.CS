using System.Collections.Generic;

namespace DRAMA.Models.TabularData
{
    public class Table<TContent>
    {
        public /* required */ Row<TContent> Header { get; init; }
        public /* required */ IEnumerable<Row<TContent>> Rows { get; init; }
        public /* required */ IEnumerable<Column<TContent>> Columns { get; init; }
    }

    public class Row<TContent>
    {
        public /* required */ int Index { get; init; }
        public /* required */ IEnumerable<Cell<TContent>> Cells { get; init; }
        public /* required */ Table<TContent> Table { get; init; }
    }

    public class Column<TContent>
    {
        public /* required */ int Index { get; init; }
        public /* required */ string Name { get; init; }
        public /* required */ IEnumerable<Cell<TContent>> Cells { get; init; }
        public /* required */ Table<TContent> Table { get; init; }
    }

    public class Cell<TContent>
    {
        public /* required */ int RowIndex { get; init; }
        public /* required */ int ColumnIndex { get; init; }
        public (int Row, int Column) Index => (RowIndex, ColumnIndex);
        public /* required */ string ColumnName { get; init; }
        public /* required */ TContent Content { get; init; }
        public /* required */ Table<TContent> Table { get; init; }
    }
}