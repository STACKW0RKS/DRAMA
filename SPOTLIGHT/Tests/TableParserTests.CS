namespace SPOTLIGHT.Tests;

[TestFixture]
public class TableParserTests
{
    [TestFixture]
    public class ParseTable
    {
        public ParseTable()
            => InitialisePlaywrightDriver().Run();

        ~ParseTable()
            => TerminatePlaywrightDriver().Run();

        private IPlaywright _driver;
        private IBrowser _browser;
        private IPage _page;
        private Uri _playground;

        private async Task InitialisePlaywrightDriver()
        {
            _driver = await Playwright.CreateAsync();
            _browser = await _driver.Firefox.LaunchAsync();
            _page = await _browser.NewPageAsync();
            _playground = new Uri(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "Resources", "Table-Parser-Unit-Testing-Playground.HTML"));

            await _page.GotoAsync(_playground.AbsoluteUri);
        }

        private async Task TerminatePlaywrightDriver()
        {
            await _page.CloseAsync();
            await _browser.CloseAsync();
            _driver.Dispose();
        }

        [Test]
        public async Task Parsing_A_Complete_Table_Generates_All_The_Correct_Cells()
        {
            Table<IElementHandle> table = await (await _page.QuerySelectorAsync("#complete")).ParseHtmlTable();

            Assert.Multiple(() =>
            {
                # region Assert Horizontal Integrity Of Text Content
                Assert.AreEqual(new[] { "0-1", "0-2", "0-3" }, table.Header.Cells.InnerText().ToArray(), "Header Cells Text");
                Assert.AreEqual(new[] { "1-1", "1-2", "1-3" }, table.Rows.RowByIndex(1).Cells.InnerText().ToArray(), "First Row Cells Text");
                Assert.AreEqual(new[] { "2-1", "2-2", "2-3" }, table.Rows.RowByIndex(2).Cells.InnerText().ToArray(), "Second Row Cells Text");
                Assert.AreEqual(new[] { "3-1", "3-2", "3-3" }, table.Rows.RowByIndex(3).Cells.InnerText().ToArray(), "Third Row Cells Text");
                Assert.AreEqual(new[] { "4-1", "4-2", "4-3" }, table.Footer.Cells.InnerText().ToArray(), "Footer Cells Text");
                # endregion

                # region Assert Vertical Integrity Of Text Content
                Assert.AreEqual("0-1", table.Columns.ColumnByIndex(1).Header.Text, "First Column Header Cell Text");
                Assert.AreEqual(new[] { "1-1", "2-1", "3-1", "4-1" }, table.Columns.ColumnByIndex(1).Cells.InnerText().ToArray(), "First Columns Cells Text");
                Assert.AreEqual("0-2", table.Columns.ColumnByIndex(2).Header.Text, "Second Column Header Cell Text");
                Assert.AreEqual(new[] { "1-2", "2-2", "3-2", "4-2" }, table.Columns.ColumnByIndex(2).Cells.InnerText().ToArray(), "Second Columns Cells Text");
                Assert.AreEqual("0-3", table.Columns.ColumnByIndex(3).Header.Text, "Third Column Header Cell Text");
                Assert.AreEqual(new[] { "1-3", "2-3", "3-3", "4-3" }, table.Columns.ColumnByIndex(3).Cells.InnerText().ToArray(), "Third Columns Cells Text");
                # endregion

                # region Assert Integrity Of Row Indices
                Assert.IsTrue(table.Header.Equals(table.HeaderBodyFooterRows().RowByIndex(0)), "Row With Index 0 Is The Header");
                Assert.IsTrue(table.Rows.First().Equals(table.Rows.RowByIndex(1)), "Row With Index 1 Is First");
                Assert.IsTrue(table.Rows.Skip(1).Take(1).Single().Equals(table.Rows.RowByIndex(2)), "Row With Index 2 Is Second");
                Assert.IsTrue(table.Rows.Last().Equals(table.Rows.RowByIndex(3)), "Row With Index 3 Is Third");
                Assert.IsTrue(table.Footer.Equals(table.HeaderBodyFooterRows().RowByIndex(4)), "Row With Index 4 Is The Footer");
                # endregion

                # region Assert Integrity Of Column Indices
                Assert.IsTrue(table.Columns.First().Equals(table.Columns.ColumnByIndex(1)), "Column With Index 1 Is First");
                Assert.IsTrue(table.Columns.Skip(1).Take(1).Single().Equals(table.Columns.ColumnByIndex(2)), "Column With Index 2 Is Second");
                Assert.IsTrue(table.Columns.Last().Equals(table.Columns.ColumnByIndex(3)), "Column With Index 3 Is Third");
                # endregion

                # region Assert Integrity Of Column Names
                Assert.AreEqual(new[] { "0-1", "0-2", "0-3" }, table.Columns.Select(column => column.Name).ToArray(), "Column Names");
                Assert.AreEqual(table.Columns.ColumnByIndex(1).Name, "0-1", "Name Of First Column");
                Assert.AreEqual(table.Columns.ColumnByIndex(2).Name, "0-2", "Name Of Second Column");
                Assert.AreEqual(table.Columns.ColumnByIndex(3).Name, "0-3", "Name Of Third Column");
                # endregion

                # region Assert Integrity Of Header Cell-To-Row And Cell-To-Column Relationships
                Assert.AreEqual((table.Header, table.Columns.ColumnByIndex(1)), (table.Cells().CellByIndex(0, 1).Row, table.Cells().CellByIndex(0, 1).Column), "Cell 0-1 Row And Column");
                Assert.AreEqual((table.Header, table.Columns.ColumnByIndex(2)), (table.Cells().CellByIndex(0, 2).Row, table.Cells().CellByIndex(0, 2).Column), "Cell 0-2 Row And Column");
                Assert.AreEqual((table.Header, table.Columns.ColumnByIndex(3)), (table.Cells().CellByIndex(0, 3).Row, table.Cells().CellByIndex(0, 3).Column), "Cell 0-3 Row And Column");
                # endregion

                # region Assert Integrity Of Body Cell-To-Row And Cell-To-Column Relationships
                Assert.AreEqual((table.Rows.RowByIndex(1), table.Columns.ColumnByIndex(1)), (table.Cells().CellByIndex(1, 1).Row, table.Cells().CellByIndex(1, 1).Column), "Cell 1-1 Row And Column");
                Assert.AreEqual((table.Rows.RowByIndex(1), table.Columns.ColumnByIndex(2)), (table.Cells().CellByIndex(1, 2).Row, table.Cells().CellByIndex(1, 2).Column), "Cell 1-2 Row And Column");
                Assert.AreEqual((table.Rows.RowByIndex(1), table.Columns.ColumnByIndex(3)), (table.Cells().CellByIndex(1, 3).Row, table.Cells().CellByIndex(1, 3).Column), "Cell 1-3 Row And Column");
                Assert.AreEqual((table.Rows.RowByIndex(2), table.Columns.ColumnByIndex(1)), (table.Cells().CellByIndex(2, 1).Row, table.Cells().CellByIndex(2, 1).Column), "Cell 2-1 Row And Column");
                Assert.AreEqual((table.Rows.RowByIndex(2), table.Columns.ColumnByIndex(2)), (table.Cells().CellByIndex(2, 2).Row, table.Cells().CellByIndex(2, 2).Column), "Cell 2-2 Row And Column");
                Assert.AreEqual((table.Rows.RowByIndex(2), table.Columns.ColumnByIndex(3)), (table.Cells().CellByIndex(2, 3).Row, table.Cells().CellByIndex(2, 3).Column), "Cell 2-3 Row And Column");
                Assert.AreEqual((table.Rows.RowByIndex(3), table.Columns.ColumnByIndex(1)), (table.Cells().CellByIndex(3, 1).Row, table.Cells().CellByIndex(3, 1).Column), "Cell 3-1 Row And Column");
                Assert.AreEqual((table.Rows.RowByIndex(3), table.Columns.ColumnByIndex(2)), (table.Cells().CellByIndex(3, 2).Row, table.Cells().CellByIndex(3, 2).Column), "Cell 3-2 Row And Column");
                Assert.AreEqual((table.Rows.RowByIndex(3), table.Columns.ColumnByIndex(3)), (table.Cells().CellByIndex(3, 3).Row, table.Cells().CellByIndex(3, 3).Column), "Cell 3-3 Row And Column");
                # endregion

                # region Assert Integrity Of Footer Cell-To-Row And Cell-To-Column Relationships
                Assert.AreEqual((table.Footer, table.Columns.ColumnByIndex(1)), (table.Cells().CellByIndex(4, 1).Row, table.Cells().CellByIndex(4, 1).Column), "Cell 4-1 Row And Column");
                Assert.AreEqual((table.Footer, table.Columns.ColumnByIndex(2)), (table.Cells().CellByIndex(4, 2).Row, table.Cells().CellByIndex(4, 2).Column), "Cell 4-2 Row And Column");
                Assert.AreEqual((table.Footer, table.Columns.ColumnByIndex(3)), (table.Cells().CellByIndex(4, 3).Row, table.Cells().CellByIndex(4, 3).Column), "Cell 4-3 Row And Column");
                # endregion

                # region Assert Cell Count
                Assert.AreEqual(table.HeaderBodyFooterRows().Count * table.Columns.Count, table.Cells().Count, "The Total Number Of Cells Equals The Number Of Rows In All Table Sections Times The Number Of Columns");
                # endregion
            });
        }

        [Test]
        public async Task Parsing_A_Table_With_No_Rows_Generates_No_Cells()
        {
            Table<IElementHandle> table = await(await _page.QuerySelectorAsync("#no-rows")).ParseHtmlTable();

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

        [Test]
        public async Task Parsing_A_Table_With_No_Sections_Generates_No_Cells()
        {
            Table<IElementHandle> table = await (await _page.QuerySelectorAsync("#no-sections")).ParseHtmlTable();

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

        [Test]
        public async Task Parsing_Table_With_No_Header_Generates_All_The_Correct_Cells()
        {
            Table<IElementHandle> table = await (await _page.QuerySelectorAsync("#no-header")).ParseHtmlTable();

            Assert.Multiple(() =>
            {
                # region Assert Presence Of Header
                Assert.IsNull(table.Header, "Table Header Is Null");
                # endregion

                # region Assert Horizontal Integrity Of Text Content
                Assert.AreEqual(new[] { "1-1", "1-2", "1-3" }, table.Rows.RowByIndex(1).Cells.InnerText().ToArray(), "First Row Cells Text");
                Assert.AreEqual(new[] { "2-1", "2-2", "2-3" }, table.Rows.RowByIndex(2).Cells.InnerText().ToArray(), "Second Row Cells Text");
                Assert.AreEqual(new[] { "3-1", "3-2", "3-3" }, table.Rows.RowByIndex(3).Cells.InnerText().ToArray(), "Third Row Cells Text");
                Assert.AreEqual(new[] { "4-1", "4-2", "4-3" }, table.Footer.Cells.InnerText().ToArray(), "Footer Cells Text");
                # endregion

                # region Assert Vertical Integrity Of Text Content
                Assert.AreEqual(new[] { "1-1", "2-1", "3-1", "4-1" }, table.Columns.ColumnByIndex(1).Cells.InnerText().ToArray(), "First Columns Cells Text");
                Assert.AreEqual(new[] { "1-2", "2-2", "3-2", "4-2" }, table.Columns.ColumnByIndex(2).Cells.InnerText().ToArray(), "Second Columns Cells Text");
                Assert.AreEqual(new[] { "1-3", "2-3", "3-3", "4-3" }, table.Columns.ColumnByIndex(3).Cells.InnerText().ToArray(), "Third Columns Cells Text");
                # endregion

                # region Assert Cell Count
                Assert.AreEqual(table.HeaderBodyFooterRows().Count * table.Columns.Count, table.Cells().Count, "The Total Number Of Cells Equals The Number Of Rows In All Table Sections Times The Number Of Columns");
                # endregion
            });
        }

        [Test]
        public async Task Parsing_Table_With_No_Body_Generates_All_The_Correct_Cells()
        {
            Table<IElementHandle> table = await (await _page.QuerySelectorAsync("#no-body")).ParseHtmlTable();

            Assert.Multiple(() =>
            {
                # region Assert Presence Of Body
                Assert.IsEmpty(table.Rows, "Collection Of Body Rows Is Empty");
                # endregion

                # region Assert Horizontal Integrity Of Text Content
                Assert.AreEqual(new[] { "0-1", "0-2", "0-3" }, table.Header.Cells.InnerText().ToArray(), "Header Cells Text");
                Assert.AreEqual(new[] { "4-1", "4-2", "4-3" }, table.Footer.Cells.InnerText().ToArray(), "Footer Cells Text");
                # endregion

                # region Assert Vertical Integrity Of Text Content
                Assert.AreEqual("0-1", table.Columns.ColumnByIndex(1).Header.Text, "First Column Header Cell Text");
                Assert.AreEqual("0-2", table.Columns.ColumnByIndex(2).Header.Text, "Second Column Header Cell Text");
                Assert.AreEqual("0-3", table.Columns.ColumnByIndex(3).Header.Text, "Third Column Header Cell Text");
                Assert.AreEqual(new[] { "4-1" }, table.Columns.ColumnByIndex(1).Cells.InnerText().ToArray(), "First Columns Cells Text");
                Assert.AreEqual(new[] { "4-2" }, table.Columns.ColumnByIndex(2).Cells.InnerText().ToArray(), "Second Columns Cells Text");
                Assert.AreEqual(new[] { "4-3" }, table.Columns.ColumnByIndex(3).Cells.InnerText().ToArray(), "Third Columns Cells Text");
                # endregion

                # region Assert Cell Count
                Assert.AreEqual(table.HeaderBodyFooterRows().Count * table.Columns.Count, table.Cells().Count, "The Total Number Of Cells Equals The Number Of Rows In All Table Sections Times The Number Of Columns");
                # endregion
            });
        }

        [Test]
        public async Task Parsing_Table_With_No_Footer_Generates_All_The_Correct_Cells()
        {
            Table<IElementHandle> table = await (await _page.QuerySelectorAsync("#no-footer")).ParseHtmlTable();

            Assert.Multiple(() =>
            {
                # region Assert Presence Of Footer
                Assert.IsNull(table.Footer, "Table Footer Is Null");
                # endregion

                # region Assert Horizontal Integrity Of Text Content
                Assert.AreEqual(new[] { "0-1", "0-2", "0-3" }, table.Header.Cells.InnerText().ToArray(), "Header Cells Text");
                Assert.AreEqual(new[] { "1-1", "1-2", "1-3" }, table.Rows.RowByIndex(1).Cells.InnerText().ToArray(), "First Row Cells Text");
                Assert.AreEqual(new[] { "2-1", "2-2", "2-3" }, table.Rows.RowByIndex(2).Cells.InnerText().ToArray(), "Second Row Cells Text");
                Assert.AreEqual(new[] { "3-1", "3-2", "3-3" }, table.Rows.RowByIndex(3).Cells.InnerText().ToArray(), "Third Row Cells Text");
                # endregion

                # region Assert Vertical Integrity Of Text Content
                Assert.AreEqual("0-1", table.Columns.ColumnByIndex(1).Header.Text, "First Column Header Cell Text");
                Assert.AreEqual("0-2", table.Columns.ColumnByIndex(2).Header.Text, "Second Column Header Cell Text");
                Assert.AreEqual("0-3", table.Columns.ColumnByIndex(3).Header.Text, "Third Column Header Cell Text");
                Assert.AreEqual(new[] { "1-1", "2-1", "3-1" }, table.Columns.ColumnByIndex(1).Cells.InnerText().ToArray(), "First Columns Cells Text");
                Assert.AreEqual(new[] { "1-2", "2-2", "3-2" }, table.Columns.ColumnByIndex(2).Cells.InnerText().ToArray(), "Second Columns Cells Text");
                Assert.AreEqual(new[] { "1-3", "2-3", "3-3" }, table.Columns.ColumnByIndex(3).Cells.InnerText().ToArray(), "Third Columns Cells Text");
                # endregion

                # region Assert Cell Count
                Assert.AreEqual(table.HeaderBodyFooterRows().Count * table.Columns.Count, table.Cells().Count, "The Total Number Of Cells Equals The Number Of Rows In All Table Sections Times The Number Of Columns");
                # endregion
            });
        }

        [Test]
        public async Task Parsing_Table_Just_With_Header_Generates_All_The_Correct_Cells()
        {
            Table<IElementHandle> table = await (await _page.QuerySelectorAsync("#just-header")).ParseHtmlTable();

            Assert.Multiple(() =>
            {
                # region Assert Presence Of Non-Header Sections
                Assert.IsEmpty(table.Rows, "Collection Of Body Rows Is Empty");
                Assert.IsNull(table.Footer, "Table Footer Is Null");
                # endregion

                # region Assert Horizontal Integrity Of Text Content
                Assert.AreEqual(new[] { "0-1", "0-2", "0-3" }, table.Header.Cells.InnerText().ToArray(), "Header Cells Text");
                # endregion

                # region Assert Vertical Integrity Of Text Content
                Assert.AreEqual("0-1", table.Columns.ColumnByIndex(1).Header.Text, "First Column Header Cell Text");
                Assert.AreEqual("0-2", table.Columns.ColumnByIndex(2).Header.Text, "Second Column Header Cell Text");
                Assert.AreEqual("0-3", table.Columns.ColumnByIndex(3).Header.Text, "Third Column Header Cell Text");
                # endregion

                # region Assert Cell Count
                Assert.AreEqual(table.HeaderBodyFooterRows().Count * table.Columns.Count, table.Cells().Count, "The Total Number Of Cells Equals The Number Of Rows In All Table Sections Times The Number Of Columns");
                # endregion
            });
        }

        [Test]
        public async Task Parsing_Table_Just_With_Body_Generates_All_The_Correct_Cells()
        {
            Table<IElementHandle> table = await (await _page.QuerySelectorAsync("#just-body")).ParseHtmlTable();

            Assert.Multiple(() =>
            {
                # region Assert Presence Of Non-Body Sections
                Assert.IsNull(table.Header, "Table Header Is Null");
                Assert.IsNull(table.Footer, "Table Footer Is Null");
                # endregion

                # region Assert Horizontal Integrity Of Text Content
                Assert.AreEqual(new[] { "1-1", "1-2", "1-3" }, table.Rows.RowByIndex(1).Cells.InnerText().ToArray(), "First Row Cells Text");
                Assert.AreEqual(new[] { "2-1", "2-2", "2-3" }, table.Rows.RowByIndex(2).Cells.InnerText().ToArray(), "Second Row Cells Text");
                Assert.AreEqual(new[] { "3-1", "3-2", "3-3" }, table.Rows.RowByIndex(3).Cells.InnerText().ToArray(), "Third Row Cells Text");
                # endregion

                # region Assert Vertical Integrity Of Text Content
                Assert.AreEqual(new[] { "1-1", "2-1", "3-1" }, table.Columns.ColumnByIndex(1).Cells.InnerText().ToArray(), "First Columns Cells Text");
                Assert.AreEqual(new[] { "1-2", "2-2", "3-2" }, table.Columns.ColumnByIndex(2).Cells.InnerText().ToArray(), "Second Columns Cells Text");
                Assert.AreEqual(new[] { "1-3", "2-3", "3-3" }, table.Columns.ColumnByIndex(3).Cells.InnerText().ToArray(), "Third Columns Cells Text");
                # endregion

                # region Assert Cell Count
                Assert.AreEqual(table.HeaderBodyFooterRows().Count * table.Columns.Count, table.Cells().Count, "The Total Number Of Cells Equals The Number Of Rows In All Table Sections Times The Number Of Columns");
                # endregion
            });
        }

        [Test]
        public async Task Parsing_Table_Just_With_Footer_Generates_All_The_Correct_Cells()
        {
            Table<IElementHandle> table = await (await _page.QuerySelectorAsync("#just-footer")).ParseHtmlTable();

            Assert.Multiple(() =>
            {
                # region Assert Presence Of Non-Footer Sections
                Assert.IsNull(table.Header, "Table Header Is Null");
                Assert.IsEmpty(table.Rows, "Collection Of Body Rows Is Empty");
                # endregion

                # region Assert Horizontal Integrity Of Text Content
                Assert.AreEqual(new[] { "4-1", "4-2", "4-3" }, table.Footer.Cells.InnerText().ToArray(), "Footer Cells Text");
                # endregion

                # region Assert Vertical Integrity Of Text Content
                Assert.AreEqual(new[] { "4-1" }, table.Columns.ColumnByIndex(1).Cells.InnerText().ToArray(), "First Columns Cells Text");
                Assert.AreEqual(new[] { "4-2" }, table.Columns.ColumnByIndex(2).Cells.InnerText().ToArray(), "Second Columns Cells Text");
                Assert.AreEqual(new[] { "4-3" }, table.Columns.ColumnByIndex(3).Cells.InnerText().ToArray(), "Third Columns Cells Text");
                # endregion

                # region Assert Cell Count
                Assert.AreEqual(table.HeaderBodyFooterRows().Count * table.Columns.Count, table.Cells().Count, "The Total Number Of Cells Equals The Number Of Rows In All Table Sections Times The Number Of Columns");
                # endregion
            });
        }
    }
}
