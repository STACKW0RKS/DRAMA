namespace SPOTLIGHT.Tests;

[TestFixture]
public class PlaywrightTableParserTests
{
    [TestFixture]
    public class ParseTable
    {
        public ParseTable()
            => Task.Run(InitialisePlaywrightDriver).Wait();

        ~ParseTable()
            => Task.Run(TerminatePlaywrightDriver).Wait();

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
                Assert.AreEqual("0-2", table.Columns.ColumnByIndex(2).Header.Text, "Second Column Header Cell Text");
                Assert.AreEqual("0-3", table.Columns.ColumnByIndex(3).Header.Text, "Third Column Header Cell Text");
                Assert.AreEqual(new[] { "1-1", "2-1", "3-1", "4-1" }, table.Columns.ColumnByIndex(1).Cells.InnerText().ToArray(), "First Columns Cells Text");
                Assert.AreEqual(new[] { "1-2", "2-2", "3-2", "4-2" }, table.Columns.ColumnByIndex(2).Cells.InnerText().ToArray(), "Second Columns Cells Text");
                Assert.AreEqual(new[] { "1-3", "2-3", "3-3", "4-3" }, table.Columns.ColumnByIndex(3).Cells.InnerText().ToArray(), "Third Columns Cells Text");
                # endregion

                # region Assert Integrity Of Row Indices
                Assert.IsTrue(table.Rows.First().Equals(table.Rows.RowByIndex(1)), "Row With Index 1 Is First");
                Assert.IsTrue(table.Rows.Skip(1).Take(1).Single().Equals(table.Rows.RowByIndex(2)), "Row With Index 2 Is Second");
                Assert.IsTrue(table.Rows.Last().Equals(table.Rows.RowByIndex(3)), "Row With Index 3 Is Last");
                # endregion

                # region Assert Integrity Of Column Indices
                Assert.IsTrue(table.Columns.First().Equals(table.Columns.ColumnByIndex(1)), "Column With Index 1 Is First");
                Assert.IsTrue(table.Columns.Skip(1).Take(1).Single().Equals(table.Columns.ColumnByIndex(2)), "Column With Index 2 Is Second");
                Assert.IsTrue(table.Columns.Last().Equals(table.Columns.ColumnByIndex(3)), "Column With Index 3 Is Last");
                # endregion

                # region Assert Integrity Of Column Names
                Assert.AreEqual(new[] { "0-1", "0-2", "0-3" }, table.Columns.Select(column => column.Name).ToArray(), "Column Names");
                # endregion

                # region Assert Integrity Of Cell-To-Row And Cell-To-Column Relationships
                Assert.AreEqual((table.Header, table.Columns.ColumnByName("0-1")), (table.Cells().CellByIndex(0, 1).Row, table.Cells().CellByIndex(0, 1).Column), "Cell 0-1 Row And Column");
                Assert.AreEqual((table.Header, table.Columns.ColumnByName("0-2")), (table.Cells().CellByIndex(0, 2).Row, table.Cells().CellByIndex(0, 2).Column), "Cell 0-2 Row And Column");
                Assert.AreEqual((table.Header, table.Columns.ColumnByName("0-3")), (table.Cells().CellByIndex(0, 3).Row, table.Cells().CellByIndex(0, 3).Column), "Cell 0-3 Row And Column");
                Assert.AreEqual((table.Rows.RowByIndex(1), table.Columns.ColumnByIndex(1)), (table.Cells().CellByIndex(1, 1).Row, table.Cells().CellByIndex(0, 1).Column), "Cell 1-1 Row And Column");
                Assert.AreEqual((table.Rows.RowByIndex(1), table.Columns.ColumnByIndex(2)), (table.Cells().CellByIndex(1, 2).Row, table.Cells().CellByIndex(0, 2).Column), "Cell 1-2 Row And Column");
                Assert.AreEqual((table.Rows.RowByIndex(1), table.Columns.ColumnByIndex(3)), (table.Cells().CellByIndex(1, 3).Row, table.Cells().CellByIndex(0, 3).Column), "Cell 1-3 Row And Column");
                # endregion

                // TODO: Complete Asserting Integrity Of Cell To Row And Cell To Column Relationships
                // TODO: Add More Tests To This General Usage Case
            });
        }

        [Test]
        public void Parsing_A_Table_With_No_Rows_Generates_No_Cells()
            => Assert.Fail();

        [Test]
        public void Parsing_A_Table_With_No_Sections_Generates_No_Cells()
            => Assert.Fail();

        [Test]
        public void Parsing_Table_With_No_Header_Generates_All_The_Correct_Cells()
            => Assert.Fail();

        [Test]
        public void Parsing_Table_With_No_Body_Generates_All_The_Correct_Cells()
            => Assert.Fail();

        [Test]
        public void Parsing_Table_With_No_Footer_Generates_All_The_Correct_Cells()
            => Assert.Fail();

        [Test]
        public void Parsing_Table_Just_With_Header_Generates_All_The_Correct_Cells()
            => Assert.Fail();

        [Test]
        public void Parsing_Table_Just_With_Body_Generates_All_The_Correct_Cells()
            => Assert.Fail();

        [Test]
        public void Parsing_Table_Just_With_Footer_Generates_All_The_Correct_Cells()
            => Assert.Fail();
    }
}
