namespace DRAMA.UnitTests.Tests.TabularData;

[TestFixture]
public class TableParserTests
{
    [TestFixture]
    public class ParseTable : TabularDataTests
    {
        [Test]
        public async Task PARSING_A_COMPLETE_TABLE_GENERATES_ALL_THE_CORRECT_CELLS()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#complete"))!.ParseHtmlTable();

                Assert.Multiple(() =>
                {
                    # region Assert Horizontal Integrity Of Text Content
                    Assert.AreEqual(new[] { "0-1", "0-2", "0-3" }, table.Header?.Cells.InnerText().ToArray(), "Header Cells Text");
                    Assert.AreEqual(new[] { "1-1", "1-2", "1-3" }, table.Rows.RowByIndex(1)?.Cells.InnerText().ToArray(), "First Row Cells Text");
                    Assert.AreEqual(new[] { "2-1", "2-2", "2-3" }, table.Rows.RowByIndex(2)?.Cells.InnerText().ToArray(), "Second Row Cells Text");
                    Assert.AreEqual(new[] { "3-1", "3-2", "3-3" }, table.Rows.RowByIndex(3)?.Cells.InnerText().ToArray(), "Third Row Cells Text");
                    Assert.AreEqual(new[] { "4-1", "4-2", "4-3" }, table.Footer?.Cells.InnerText().ToArray(), "Footer Cells Text");
                    # endregion

                    # region Assert Vertical Integrity Of Text Content
                    Assert.AreEqual("0-1", table.Columns.ColumnByIndex(1)?.Header?.Text, "First Column Header Cell Text");
                    Assert.AreEqual("0-2", table.Columns.ColumnByIndex(2)?.Header?.Text, "Second Column Header Cell Text");
                    Assert.AreEqual("0-3", table.Columns.ColumnByIndex(3)?.Header?.Text, "Third Column Header Cell Text");
                    Assert.AreEqual(new[] { "1-1", "2-1", "3-1" }, table.Columns.ColumnByIndex(1)?.Cells.InnerText().ToArray(), "First Columns Cells Text");
                    Assert.AreEqual(new[] { "1-2", "2-2", "3-2" }, table.Columns.ColumnByIndex(2)?.Cells.InnerText().ToArray(), "Second Columns Cells Text");
                    Assert.AreEqual(new[] { "1-3", "2-3", "3-3" }, table.Columns.ColumnByIndex(3)?.Cells.InnerText().ToArray(), "Third Columns Cells Text");
                    Assert.AreEqual("4-1", table.Columns.ColumnByIndex(1)?.Footer?.Text, "First Column Footer Cell Text");
                    Assert.AreEqual("4-2", table.Columns.ColumnByIndex(2)?.Footer?.Text, "Second Column Footer Cell Text");
                    Assert.AreEqual("4-3", table.Columns.ColumnByIndex(3)?.Footer?.Text, "Third Column Footer Cell Text");
                    # endregion

                    # region Assert Integrity Of Row Indices
                    Assert.IsTrue(table.Header is not null && table.Header.Equals(table.HeaderBodyFooterRows().RowByIndex(0)), "Row With Index 0 Is The Header");
                    Assert.IsTrue(table.Rows.First().Equals(table.Rows.RowByIndex(1)), "Row With Index 1 Is First");
                    Assert.IsTrue(table.Rows.Skip(1).Take(1).Single().Equals(table.Rows.RowByIndex(2)), "Row With Index 2 Is Second");
                    Assert.IsTrue(table.Rows.Last().Equals(table.Rows.RowByIndex(3)), "Row With Index 3 Is Third");
                    Assert.IsTrue(table.Footer is not null && table.Footer.Equals(table.HeaderBodyFooterRows().RowByIndex(4)), "Row With Index 4 Is The Footer");
                    # endregion

                    # region Assert Integrity Of Column Indices
                    Assert.IsTrue(table.Columns.First().Equals(table.Columns.ColumnByIndex(1)), "Column With Index 1 Is First");
                    Assert.IsTrue(table.Columns.Skip(1).Take(1).Single().Equals(table.Columns.ColumnByIndex(2)), "Column With Index 2 Is Second");
                    Assert.IsTrue(table.Columns.Last().Equals(table.Columns.ColumnByIndex(3)), "Column With Index 3 Is Third");
                    # endregion

                    # region Assert Integrity Of Column Names
                    Assert.AreEqual(new[] { "0-1", "0-2", "0-3" }, table.Columns.Select(column => column.Name).ToArray(), "Column Names");
                    Assert.AreEqual(table.Columns.ColumnByIndex(1)?.Name, "0-1", "Name Of First Column");
                    Assert.AreEqual(table.Columns.ColumnByIndex(2)?.Name, "0-2", "Name Of Second Column");
                    Assert.AreEqual(table.Columns.ColumnByIndex(3)?.Name, "0-3", "Name Of Third Column");
                    # endregion

                    # region Assert Integrity Of Header Cell-To-Row And Cell-To-Column Relationships
                    Assert.AreEqual((table.Header, table.Columns.ColumnByIndex(1)), (table.Cells().CellByIndex(0, 1)?.Row, table.Cells().CellByIndex(0, 1)?.Column), "Cell 0-1 Row And Column");
                    Assert.AreEqual((table.Header, table.Columns.ColumnByIndex(2)), (table.Cells().CellByIndex(0, 2)?.Row, table.Cells().CellByIndex(0, 2)?.Column), "Cell 0-2 Row And Column");
                    Assert.AreEqual((table.Header, table.Columns.ColumnByIndex(3)), (table.Cells().CellByIndex(0, 3)?.Row, table.Cells().CellByIndex(0, 3)?.Column), "Cell 0-3 Row And Column");
                    # endregion

                    # region Assert Integrity Of Body Cell-To-Row And Cell-To-Column Relationships
                    Assert.AreEqual((table.Rows.RowByIndex(1), table.Columns.ColumnByIndex(1)), (table.Cells().CellByIndex(1, 1)?.Row, table.Cells().CellByIndex(1, 1)?.Column), "Cell 1-1 Row And Column");
                    Assert.AreEqual((table.Rows.RowByIndex(1), table.Columns.ColumnByIndex(2)), (table.Cells().CellByIndex(1, 2)?.Row, table.Cells().CellByIndex(1, 2)?.Column), "Cell 1-2 Row And Column");
                    Assert.AreEqual((table.Rows.RowByIndex(1), table.Columns.ColumnByIndex(3)), (table.Cells().CellByIndex(1, 3)?.Row, table.Cells().CellByIndex(1, 3)?.Column), "Cell 1-3 Row And Column");
                    Assert.AreEqual((table.Rows.RowByIndex(2), table.Columns.ColumnByIndex(1)), (table.Cells().CellByIndex(2, 1)?.Row, table.Cells().CellByIndex(2, 1)?.Column), "Cell 2-1 Row And Column");
                    Assert.AreEqual((table.Rows.RowByIndex(2), table.Columns.ColumnByIndex(2)), (table.Cells().CellByIndex(2, 2)?.Row, table.Cells().CellByIndex(2, 2)?.Column), "Cell 2-2 Row And Column");
                    Assert.AreEqual((table.Rows.RowByIndex(2), table.Columns.ColumnByIndex(3)), (table.Cells().CellByIndex(2, 3)?.Row, table.Cells().CellByIndex(2, 3)?.Column), "Cell 2-3 Row And Column");
                    Assert.AreEqual((table.Rows.RowByIndex(3), table.Columns.ColumnByIndex(1)), (table.Cells().CellByIndex(3, 1)?.Row, table.Cells().CellByIndex(3, 1)?.Column), "Cell 3-1 Row And Column");
                    Assert.AreEqual((table.Rows.RowByIndex(3), table.Columns.ColumnByIndex(2)), (table.Cells().CellByIndex(3, 2)?.Row, table.Cells().CellByIndex(3, 2)?.Column), "Cell 3-2 Row And Column");
                    Assert.AreEqual((table.Rows.RowByIndex(3), table.Columns.ColumnByIndex(3)), (table.Cells().CellByIndex(3, 3)?.Row, table.Cells().CellByIndex(3, 3)?.Column), "Cell 3-3 Row And Column");
                    # endregion

                    # region Assert Integrity Of Footer Cell-To-Row And Cell-To-Column Relationships
                    Assert.AreEqual((table.Footer, table.Columns.ColumnByIndex(1)), (table.Cells().CellByIndex(4, 1)?.Row, table.Cells().CellByIndex(4, 1)?.Column), "Cell 4-1 Row And Column");
                    Assert.AreEqual((table.Footer, table.Columns.ColumnByIndex(2)), (table.Cells().CellByIndex(4, 2)?.Row, table.Cells().CellByIndex(4, 2)?.Column), "Cell 4-2 Row And Column");
                    Assert.AreEqual((table.Footer, table.Columns.ColumnByIndex(3)), (table.Cells().CellByIndex(4, 3)?.Row, table.Cells().CellByIndex(4, 3)?.Column), "Cell 4-3 Row And Column");
                    # endregion

                    # region Assert Cell Count
                    Assert.AreEqual(table.HeaderBodyFooterRows().Count * table.Columns.Count, table.Cells().Count, "The Total Number Of Cells Equals The Number Of Rows In All Table Sections Times The Number Of Columns");
                    # endregion
                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task PARSING_A_TABLE_WITH_NO_ROWS_GENERATES_NO_CELLS()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await(await Page.QuerySelectorAsync("#no-rows")).ParseHtmlTable();

                Assert.Multiple(() =>
                {
                    # region Assert Presence Of Rows
                    Assert.IsNull(table.Header, "Table Header Is Null");
                    Assert.IsEmpty(table.Rows, "Collection Of Body Rows Is Empty");
                    Assert.IsEmpty(table.HeaderBodyFooterRows(), "Collection Of Header, Body, And Footer Rows Is Empty");
                    Assert.IsNull(table.Footer, "Table Footer Is Null");
                    # endregion

                    # region Assert Presence Of Cells
                    Assert.IsEmpty(table.Cells(), "Collection Of Table Cells Is Empty");
                    # endregion

                    # region Assert Cell Count
                    Assert.AreEqual(table.HeaderBodyFooterRows().Count * table.Columns.Count, table.Cells().Count, "The Total Number Of Cells Equals The Number Of Rows In All Table Sections Times The Number Of Columns");
                    # endregion
                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task PARSING_A_TABLE_WITH_NO_SECTIONS_GENERATES_NO_CELLS()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#no-sections")).ParseHtmlTable();

                Assert.Multiple(() =>
                {
                    # region Assert Presence Of Rows
                    Assert.IsNull(table.Header, "Table Header Is Null");
                    Assert.IsEmpty(table.Rows, "Collection Of Body Rows Is Empty");
                    Assert.IsEmpty(table.HeaderBodyFooterRows(), "Collection Of Header, Body, And Footer Rows Is Empty");
                    Assert.IsNull(table.Footer, "Table Footer Is Null");
                    # endregion

                    # region Assert Presence Of Cells
                    Assert.IsEmpty(table.Cells(), "Collection Of Table Cells Is Empty");
                    # endregion

                    # region Assert Cell Count
                    Assert.AreEqual(table.HeaderBodyFooterRows().Count * table.Columns.Count, table.Cells().Count, "The Total Number Of Cells Equals The Number Of Rows In All Table Sections Times The Number Of Columns");
                    # endregion
                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task PARSING_TABLE_WITH_NO_HEADER_GENERATES_ALL_THE_CORRECT_CELLS()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#no-header")).ParseHtmlTable();

                Assert.Multiple(() =>
                {
                    # region Assert Presence Of Header
                    Assert.IsNull(table.Header, "Table Header Is Null");
                    # endregion

                    # region Assert Horizontal Integrity Of Text Content
                    Assert.AreEqual(new[] { "1-1", "1-2", "1-3" }, table.Rows.RowByIndex(1)?.Cells.InnerText().ToArray(), "First Row Cells Text");
                    Assert.AreEqual(new[] { "2-1", "2-2", "2-3" }, table.Rows.RowByIndex(2)?.Cells.InnerText().ToArray(), "Second Row Cells Text");
                    Assert.AreEqual(new[] { "3-1", "3-2", "3-3" }, table.Rows.RowByIndex(3)?.Cells.InnerText().ToArray(), "Third Row Cells Text");
                    Assert.AreEqual(new[] { "4-1", "4-2", "4-3" }, table.Footer?.Cells.InnerText().ToArray(), "Footer Cells Text");
                    # endregion

                    # region Assert Vertical Integrity Of Text Content
                    Assert.AreEqual(new[] { "1-1", "2-1", "3-1" }, table.Columns.ColumnByIndex(1)?.Cells.InnerText().ToArray(), "First Columns Cells Text");
                    Assert.AreEqual(new[] { "1-2", "2-2", "3-2" }, table.Columns.ColumnByIndex(2)?.Cells.InnerText().ToArray(), "Second Columns Cells Text");
                    Assert.AreEqual(new[] { "1-3", "2-3", "3-3" }, table.Columns.ColumnByIndex(3)?.Cells.InnerText().ToArray(), "Third Columns Cells Text");
                    Assert.AreEqual("4-1", table.Columns.ColumnByIndex(1)?.Footer?.Text, "First Column Footer Cell Text");
                    Assert.AreEqual("4-2", table.Columns.ColumnByIndex(2)?.Footer?.Text, "Second Column Footer Cell Text");
                    Assert.AreEqual("4-3", table.Columns.ColumnByIndex(3)?.Footer?.Text, "Third Column Footer Cell Text");
                    # endregion

                    # region Assert Cell Count
                    Assert.AreEqual(table.HeaderBodyFooterRows().Count * table.Columns.Count, table.Cells().Count, "The Total Number Of Cells Equals The Number Of Rows In All Table Sections Times The Number Of Columns");
                    # endregion
                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task PARSING_TABLE_WITH_NO_BODY_GENERATES_ALL_THE_CORRECT_CELLS()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#no-body")).ParseHtmlTable();

                Assert.Multiple(() =>
                {
                    # region Assert Presence Of Body
                    Assert.IsEmpty(table.Rows, "Collection Of Body Rows Is Empty");
                    # endregion

                    # region Assert Horizontal Integrity Of Text Content
                    Assert.AreEqual(new[] { "0-1", "0-2", "0-3" }, table.Header?.Cells.InnerText().ToArray(), "Header Cells Text");
                    Assert.AreEqual(new[] { "4-1", "4-2", "4-3" }, table.Footer?.Cells.InnerText().ToArray(), "Footer Cells Text");
                    # endregion

                    # region Assert Vertical Integrity Of Text Content
                    Assert.AreEqual("0-1", table.Columns.ColumnByIndex(1)?.Header?.Text, "First Column Header Cell Text");
                    Assert.AreEqual("0-2", table.Columns.ColumnByIndex(2)?.Header?.Text, "Second Column Header Cell Text");
                    Assert.AreEqual("0-3", table.Columns.ColumnByIndex(3)?.Header?.Text, "Third Column Header Cell Text");
                    Assert.AreEqual("4-1", table.Columns.ColumnByIndex(1)?.Footer?.Text, "First Column Footer Cell Text");
                    Assert.AreEqual("4-2", table.Columns.ColumnByIndex(2)?.Footer?.Text, "Second Column Footer Cell Text");
                    Assert.AreEqual("4-3", table.Columns.ColumnByIndex(3)?.Footer?.Text, "Third Column Footer Cell Text");
                    # endregion

                    # region Assert Cell Count
                    Assert.AreEqual(table.HeaderBodyFooterRows().Count * table.Columns.Count, table.Cells().Count, "The Total Number Of Cells Equals The Number Of Rows In All Table Sections Times The Number Of Columns");
                    # endregion
                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task PARSING_TABLE_WITH_NO_FOOTER_GENERATES_ALL_THE_CORRECT_CELLS()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#no-footer")).ParseHtmlTable();

                Assert.Multiple(() =>
                {
                    # region Assert Presence Of Footer
                    Assert.IsNull(table.Footer, "Table Footer Is Null");
                    # endregion

                    # region Assert Horizontal Integrity Of Text Content
                    Assert.AreEqual(new[] { "0-1", "0-2", "0-3" }, table.Header?.Cells.InnerText().ToArray(), "Header Cells Text");
                    Assert.AreEqual(new[] { "1-1", "1-2", "1-3" }, table.Rows.RowByIndex(1)?.Cells.InnerText().ToArray(), "First Row Cells Text");
                    Assert.AreEqual(new[] { "2-1", "2-2", "2-3" }, table.Rows.RowByIndex(2)?.Cells.InnerText().ToArray(), "Second Row Cells Text");
                    Assert.AreEqual(new[] { "3-1", "3-2", "3-3" }, table.Rows.RowByIndex(3)?.Cells.InnerText().ToArray(), "Third Row Cells Text");
                    # endregion

                    # region Assert Vertical Integrity Of Text Content
                    Assert.AreEqual("0-1", table.Columns.ColumnByIndex(1)?.Header?.Text, "First Column Header Cell Text");
                    Assert.AreEqual("0-2", table.Columns.ColumnByIndex(2)?.Header?.Text, "Second Column Header Cell Text");
                    Assert.AreEqual("0-3", table.Columns.ColumnByIndex(3)?.Header?.Text, "Third Column Header Cell Text");
                    Assert.AreEqual(new[] { "1-1", "2-1", "3-1" }, table.Columns.ColumnByIndex(1)?.Cells.InnerText().ToArray(), "First Columns Cells Text");
                    Assert.AreEqual(new[] { "1-2", "2-2", "3-2" }, table.Columns.ColumnByIndex(2)?.Cells.InnerText().ToArray(), "Second Columns Cells Text");
                    Assert.AreEqual(new[] { "1-3", "2-3", "3-3" }, table.Columns.ColumnByIndex(3)?.Cells.InnerText().ToArray(), "Third Columns Cells Text");
                    # endregion

                    # region Assert Cell Count
                    Assert.AreEqual(table.HeaderBodyFooterRows().Count * table.Columns.Count, table.Cells().Count, "The Total Number Of Cells Equals The Number Of Rows In All Table Sections Times The Number Of Columns");
                    # endregion
                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task PARSING_TABLE_JUST_WITH_HEADER_GENERATES_ALL_THE_CORRECT_CELLS()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#just-header")).ParseHtmlTable();

                Assert.Multiple(() =>
                {
                    # region Assert Presence Of Non-Header Sections
                    Assert.IsEmpty(table.Rows, "Collection Of Body Rows Is Empty");
                    Assert.IsNull(table.Footer, "Table Footer Is Null");
                    # endregion

                    # region Assert Horizontal Integrity Of Text Content
                    Assert.AreEqual(new[] { "0-1", "0-2", "0-3" }, table.Header?.Cells.InnerText().ToArray(), "Header Cells Text");
                    # endregion

                    # region Assert Vertical Integrity Of Text Content
                    Assert.AreEqual("0-1", table.Columns.ColumnByIndex(1)?.Header?.Text, "First Column Header Cell Text");
                    Assert.AreEqual("0-2", table.Columns.ColumnByIndex(2)?.Header?.Text, "Second Column Header Cell Text");
                    Assert.AreEqual("0-3", table.Columns.ColumnByIndex(3)?.Header?.Text, "Third Column Header Cell Text");
                    # endregion

                    # region Assert Cell Count
                    Assert.AreEqual(table.HeaderBodyFooterRows().Count * table.Columns.Count, table.Cells().Count, "The Total Number Of Cells Equals The Number Of Rows In All Table Sections Times The Number Of Columns");
                    # endregion
                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task PARSING_TABLE_JUST_WITH_BODY_GENERATES_ALL_THE_CORRECT_CELLS()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#just-body")).ParseHtmlTable();

                Assert.Multiple(() =>
                {
                    # region Assert Presence Of Non-Body Sections
                    Assert.IsNull(table.Header, "Table Header Is Null");
                    Assert.IsNull(table.Footer, "Table Footer Is Null");
                    # endregion

                    # region Assert Horizontal Integrity Of Text Content
                    Assert.AreEqual(new[] { "1-1", "1-2", "1-3" }, table.Rows.RowByIndex(1)?.Cells.InnerText().ToArray(), "First Row Cells Text");
                    Assert.AreEqual(new[] { "2-1", "2-2", "2-3" }, table.Rows.RowByIndex(2)?.Cells.InnerText().ToArray(), "Second Row Cells Text");
                    Assert.AreEqual(new[] { "3-1", "3-2", "3-3" }, table.Rows.RowByIndex(3)?.Cells.InnerText().ToArray(), "Third Row Cells Text");
                    # endregion

                    # region Assert Vertical Integrity Of Text Content
                    Assert.AreEqual(new[] { "1-1", "2-1", "3-1" }, table.Columns.ColumnByIndex(1)?.Cells.InnerText().ToArray(), "First Columns Cells Text");
                    Assert.AreEqual(new[] { "1-2", "2-2", "3-2" }, table.Columns.ColumnByIndex(2)?.Cells.InnerText().ToArray(), "Second Columns Cells Text");
                    Assert.AreEqual(new[] { "1-3", "2-3", "3-3" }, table.Columns.ColumnByIndex(3)?.Cells.InnerText().ToArray(), "Third Columns Cells Text");
                    # endregion

                    # region Assert Cell Count
                    Assert.AreEqual(table.HeaderBodyFooterRows().Count * table.Columns.Count, table.Cells().Count, "The Total Number Of Cells Equals The Number Of Rows In All Table Sections Times The Number Of Columns");
                    # endregion
                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task PARSING_TABLE_JUST_WITH_FOOTER_GENERATES_ALL_THE_CORRECT_CELLS()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#just-footer")).ParseHtmlTable();

                Assert.Multiple(() =>
                {
                    # region Assert Presence Of Non-Footer Sections
                    Assert.IsNull(table.Header, "Table Header Is Null");
                    Assert.IsEmpty(table.Rows, "Collection Of Body Rows Is Empty");
                    # endregion

                    # region Assert Horizontal Integrity Of Text Content
                    Assert.AreEqual(new[] { "4-1", "4-2", "4-3" }, table.Footer?.Cells.InnerText().ToArray(), "Footer Cells Text");
                    # endregion

                    # region Assert Vertical Integrity Of Text Content
                    Assert.AreEqual("4-1", table.Columns.ColumnByIndex(1)?.Footer?.Text, "First Column Footer Cell Text");
                    Assert.AreEqual("4-2", table.Columns.ColumnByIndex(2)?.Footer?.Text, "Second Column Footer Cell Text");
                    Assert.AreEqual("4-3", table.Columns.ColumnByIndex(3)?.Footer?.Text, "Third Column Footer Cell Text");
                    # endregion

                    # region Assert Cell Count
                    Assert.AreEqual(table.HeaderBodyFooterRows().Count * table.Columns.Count, table.Cells().Count, "The Total Number Of Cells Equals The Number Of Rows In All Table Sections Times The Number Of Columns");
                    # endregion
                });
            }

            else Assert.Fail("IPage Not Initialised");
        }
    }
}
