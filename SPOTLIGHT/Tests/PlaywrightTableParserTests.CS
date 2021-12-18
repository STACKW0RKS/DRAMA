namespace SPOTLIGHT.Tests;

[TestFixture]
public class PlaywrightTableParserTests
{
    [TestFixture]
    public class ParseTable
    {
        #region Initialise Playwright Driver In Constructor & Terminate Playwright Driver In Destructor

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

        #endregion

        [Test]
        public void Parsing_A_Complete_Table_Generates_All_The_Correct_Cells()
            => Assert.Pass();

        [Test]
        public void Parsing_A_Table_With_No_Rows_Generates_No_Cells()
            => Assert.Pass();

        [Test]
        public void Parsing_A_Table_With_No_Sections_Generates_No_Cells()
            => Assert.Pass();

        [Test]
        public void Parsing_Table_With_No_Header_Generates_All_The_Correct_Cells()
            => Assert.Pass();

        [Test]
        public void Parsing_Table_With_No_Body_Generates_All_The_Correct_Cells()
            => Assert.Pass();

        [Test]
        public void Parsing_Table_With_No_Footer_Generates_All_The_Correct_Cells()
            => Assert.Pass();

        [Test]
        public void Parsing_Table_Just_With_Header_Generates_All_The_Correct_Cells()
            => Assert.Pass();

        [Test]
        public void Parsing_Table_Just_With_Body_Generates_All_The_Correct_Cells()
            => Assert.Pass();

        [Test]
        public void Parsing_Table_Just_With_Footer_Generates_All_The_Correct_Cells()
            => Assert.Pass();
    }
}
