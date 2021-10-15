using System.Collections.Generic;
using System.Linq;

namespace DRAMA.Models.TabularData
{
    public class Table<TContent>
    {
        /// <summary>
        ///     Creates a table with content of the specified type.
        ///     <br/>
        ///     The header, rows, and columns are not fully defined, and the Header, Rows, and Columns properties need to be set
        ///     post object construction.
        /// </summary>
        public Table()
        {
            Rows = Enumerable.Empty<Row<TContent>>();
            Columns = Enumerable.Empty<Column<TContent>>();
        }

        /// <summary>
        ///     Creates a table with content of the specified type.
        ///     <br/>
        ///     The header, rows, and columns are already defined, and passed as parameters to the constructor.
        /// </summary>
        public Table(Row<TContent> header, IEnumerable<Row<TContent>> rows, IEnumerable<Column<TContent>> columns)
        {
            Header = header;
            Rows = rows;
            Columns = columns;
        }

        public Row<TContent> Header { get; private set; }
        public IEnumerable<Row<TContent>> Rows { get; private set; }
        public IEnumerable<Column<TContent>> Columns { get; private set; }

        /// <summary>
        ///     Sets the header of this table.
        /// </summary>
        public void SetHeader(Row<TContent> row)
            => Header = row;

        /// <summary>
        ///     Sets the collection of rows in this table.
        /// </summary>
        public void SetRows(IEnumerable<Row<TContent>> rows)
            => Rows = rows;

        /// <summary>
        ///     Adds a row to the collection of rows in this table.
        /// </summary>
        public void AddRow(Row<TContent> row)
            => Rows = Rows.Append(row);

        /// <summary>
        ///     Sets the collection of columns in this table.
        /// </summary>
        public void SetColumns(IEnumerable<Column<TContent>> columns)
            => Columns = columns;

        /// <summary>
        ///     Adds a column to the collection of columns in this table.
        /// </summary>
        public void AddColumn(Column<TContent> column)
            => Columns = Columns.Append(column);
    }
}