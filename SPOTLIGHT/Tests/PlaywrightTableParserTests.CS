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
                Assert.AreEqual(new[] { "0-1", "0-2", "0-3" }, table.Header.Cells.InnerText().ToArray(), "Header Cells Text");
                Assert.AreEqual(new[] { "1-1", "1-2", "1-3" }, table.Rows.RowByIndex(1).Cells.InnerText().ToArray(), "First Row Cells Text");
                Assert.AreEqual(new[] { "2-1", "2-2", "2-3" }, table.Rows.RowByIndex(2).Cells.InnerText().ToArray(), "Second Row Cells Text");
                Assert.AreEqual(new[] { "3-1", "3-2", "3-3" }, table.Rows.RowByIndex(3).Cells.InnerText().ToArray(), "Third Row Cells Text");
                Assert.AreEqual(new[] { "4-1", "4-2", "4-3" }, table.Footer.Cells.InnerText().ToArray(), "Footer Cells Text");

                Assert.AreEqual(new[] { "0-1", "0-2", "0-3" }, table.Columns.Select(column => column.Name).ToArray(), "Column Names");

                Assert.IsTrue(table.Rows.First().Equals(table.Rows.RowByIndex(1)), "Row With Index 1 Is First");
                Assert.IsTrue(table.Rows.Skip(1).Take(1).Single().Equals(table.Rows.RowByIndex(2)), "Row With Index 2 Is Second");
                Assert.IsTrue(table.Rows.Last().Equals(table.Rows.RowByIndex(3)), "Row With Index 3 Is Last");

                Assert.IsTrue(table.Columns.First().Equals(table.Columns.ColumnByIndex(1)), "Column With Index 1 Is First");
                Assert.IsTrue(table.Columns.Skip(1).Take(1).Single().Equals(table.Columns.ColumnByIndex(2)), "Column With Index 2 Is Second");
                Assert.IsTrue(table.Columns.Last().Equals(table.Columns.ColumnByIndex(3)), "Column With Index 3 Is Last");

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
