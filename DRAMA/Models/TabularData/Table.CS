using System.Collections.Generic;

namespace DRAMA.Models.TabularData
{
    public class Table<TContent>
    {
        /// <summary>
        ///     Creates a table with content of the specified type.
        ///     <br/>
        ///     The header, rows, and columns are not fully defined, and the Header, Rows, and Columns properties need to be set post object construction.
        /// </summary>
        public Table()
        {
            Header = new Row<TContent>(0, this);
            Rows = new List<Row<TContent>>();
            Columns = new List<Column<TContent>>();
        }

        /// <summary>
        ///     Creates a table with content of the specified type.
        ///     <br/>
        ///     The header, rows, and columns are already defined, and passed as parameters to the constructor.
        /// </summary>
        public Table(Row<TContent> header, List<Row<TContent>> rows, List<Column<TContent>> columns)
        {
            Header = header;
            Rows = rows;
            Columns = columns;
        }

        public Row<TContent> Header { get; private set; }
        public List<Row<TContent>> Rows { get; private set; }
        public List<Column<TContent>> Columns { get; private set; }

        /// <summary>
        ///     Sets the header of this table.
        /// </summary>
        public void SetHeader(Row<TContent> row)
            => Header = row;

        /// <summary>
        ///     Sets the collection of rows in this table.
        /// </summary>
        public void SetRows(List<Row<TContent>> rows)
            => Rows = rows;

        /// <summary>
        ///     Adds a row to the collection of rows in this table.
        /// </summary>
        public void AddRow(Row<TContent> row)
            => Rows.Add(row);

        /// <summary>
        ///     Adds rows to the collection of rows in this table.
        /// </summary>
        public void AddRows(List<Row<TContent>> rows)
            => Rows.AddRange(rows);

        /// <summary>
        ///     Sets the collection of columns in this table.
        /// </summary>
        public void SetColumns(List<Column<TContent>> columns)
            => Columns = columns;

        /// <summary>
        ///     Adds a column to the collection of columns in this table.
        /// </summary>
        public void AddColumn(Column<TContent> column)
            => Columns.Add(column);

        /// <summary>
        ///     Adds columns to the collection of columns in this table.
        /// </summary>
        public void AddColumns(List<Column<TContent>> columns)
            => Columns.AddRange(columns);
    }
}