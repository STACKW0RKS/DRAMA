namespace DRAMA.Models.TabularData
{
    public class Cell<TContent>
    {
        /// <summary>
        ///     Creates a table cell with content of the specified type.
        /// </summary>
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