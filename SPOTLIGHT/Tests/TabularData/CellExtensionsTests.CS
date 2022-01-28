namespace SPOTLIGHT.Tests.TabularData;

[TestFixture]
public class CellExtensionsTests
{
    [TestFixture]
    public class Content : TabularDataTests
    {
        [Test]
        public async Task Row_Content_Returns_Content_Of_All_Cells_In_The_Row()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#complete"))!.ParseHtmlTable();

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(table.Header?.Cells.Select(cell => cell.Content), table.Header?.Cells.Content(), "Header Cells Content");
                    Assert.AreEqual(table.Rows.RowByIndex(1)?.Cells.Select(cell => cell.Content), table.Rows.RowByIndex(1)?.Cells.Content(), "First Row Cells Content");
                    Assert.AreEqual(table.Rows.RowByIndex(2)?.Cells.Select(cell => cell.Content), table.Rows.RowByIndex(2)?.Cells.Content(), "First Row Cells Content");
                    Assert.AreEqual(table.Rows.RowByIndex(3)?.Cells.Select(cell => cell.Content), table.Rows.RowByIndex(3)?.Cells.Content(), "First Row Cells Content");
                    Assert.AreEqual(table.Footer?.Cells.Select(cell => cell.Content), table.Footer?.Cells.Content(), "Footer Cells Content");
                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task Column_Content_Returns_Content_Of_All_Cells_In_The_Column()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#complete"))!.ParseHtmlTable();

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(table.Columns.ColumnByIndex(1)?.Cells.Select(cell => cell.Content), table.Columns.ColumnByIndex(1)?.Cells.Content(), "First Columns Cells Content");
                    Assert.AreEqual(table.Columns.ColumnByIndex(2)?.Cells.Select(cell => cell.Content), table.Columns.ColumnByIndex(2)?.Cells.Content(), "First Columns Cells Content");
                    Assert.AreEqual(table.Columns.ColumnByIndex(3)?.Cells.Select(cell => cell.Content), table.Columns.ColumnByIndex(3)?.Cells.Content(), "First Columns Cells Content");
                });
            }

            else Assert.Fail("IPage Not Initialised");
        }
    }

    [TestFixture]
    public class InnerText : TabularDataTests
    {
        [Test]
        public async Task Row_Inner_Text_Returns_Inner_Text_Of_All_Cells_In_The_Row()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#complete"))!.ParseHtmlTable();

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(table.Header?.Cells.Select(cell => cell.Text).ToList(), table.Header?.Cells.InnerText(), "Header Cells Inner Text");
                    Assert.AreEqual(table.Rows.RowByIndex(1)?.Cells.Select(cell => cell.Text).ToList(), table.Rows.RowByIndex(1)?.Cells.InnerText(), "First Row Cells Inner Text");
                    Assert.AreEqual(table.Rows.RowByIndex(2)?.Cells.Select(cell => cell.Text).ToList(), table.Rows.RowByIndex(2)?.Cells.InnerText(), "Second Row Cells Inner Text");
                    Assert.AreEqual(table.Rows.RowByIndex(3)?.Cells.Select(cell => cell.Text).ToList(), table.Rows.RowByIndex(3)?.Cells.InnerText(), "Third Row Cells Inner Text");
                    Assert.AreEqual(table.Footer?.Cells.Select(cell => cell.Text).ToList(), table.Footer?.Cells.InnerText(), "Footer Cells Inner Text");
                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task Column_Inner_Text_Returns_Inner_Text_Of_All_Cells_In_The_Column()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#complete"))!.ParseHtmlTable();

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(table.Columns.ColumnByIndex(1)?.Cells.Select(cell => cell.Text).ToList(), table.Columns.ColumnByIndex(1)?.Cells.InnerText(), "First Columns Cells Inner Text");
                    Assert.AreEqual(table.Columns.ColumnByIndex(2)?.Cells.Select(cell => cell.Text).ToList(), table.Columns.ColumnByIndex(2)?.Cells.InnerText(), "Second Columns Cells Inner Text");
                    Assert.AreEqual(table.Columns.ColumnByIndex(3)?.Cells.Select(cell => cell.Text).ToList(), table.Columns.ColumnByIndex(3)?.Cells.InnerText(), "Third Columns Cells Inner Text");
                });
            }

            else Assert.Fail("IPage Not Initialised");
        }
    }

    [TestFixture]
    public class CellQuery : TabularDataTests
    {
        [Test]
        public async Task Cell_By_Row_Index_Returns_The_Expected_Cell()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await(await Page.QuerySelectorAsync("#complete"))!.ParseHtmlTable();
                int randomColumnIndex = table.Columns.RandomElement().Index;

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(table.Columns.ColumnByIndex(randomColumnIndex)?.Header, table.Columns.ColumnByIndex(randomColumnIndex)?.Cells.CellByRowIndex(0), $"Header Cell Of Column {randomColumnIndex} Is On Row Index 0");
                    Assert.AreEqual(table.Columns.ColumnByIndex(randomColumnIndex)?.Cells[0], table.Columns.ColumnByIndex(randomColumnIndex)?.Cells.CellByRowIndex(1), $"First Row Cell Of Column {randomColumnIndex} Is On Row Index 1");
                    Assert.AreEqual(table.Columns.ColumnByIndex(randomColumnIndex)?.Cells[1], table.Columns.ColumnByIndex(randomColumnIndex)?.Cells.CellByRowIndex(2), $"Second Row Cell Of Column {randomColumnIndex} Is On Row Index 2");
                    Assert.AreEqual(table.Columns.ColumnByIndex(randomColumnIndex)?.Cells[2], table.Columns.ColumnByIndex(randomColumnIndex)?.Cells.CellByRowIndex(3), $"Third Row Cell Of Column {randomColumnIndex} Is On Row Index 3");
                    Assert.AreEqual(table.Columns.ColumnByIndex(randomColumnIndex)?.Footer, table.Columns.ColumnByIndex(randomColumnIndex)?.Cells.CellByRowIndex(4), $"Footer Cell Of Column {randomColumnIndex} Is On Row Index 4");
                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task Cells_By_Row_Index_Returns_The_Expected_Cells()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#complete"))!.ParseHtmlTable();

                Assert.Multiple(() =>
                {

                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task Cell_By_Column_Index_Returns_The_Expected_Cell()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#complete"))!.ParseHtmlTable();

                Assert.Multiple(() =>
                {

                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task Cells_By_Column_Index_Returns_The_Expected_Cells()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#complete"))!.ParseHtmlTable();

                Assert.Multiple(() =>
                {

                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task Cell_By_Column_Name_Returns_The_Expected_Cell()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#complete"))!.ParseHtmlTable();

                Assert.Multiple(() =>
                {

                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task Cells_By_Column_Name_Returns_The_Expected_Cells()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#complete"))!.ParseHtmlTable();

                Assert.Multiple(() =>
                {

                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task Cell_By_Index_Returns_The_Expected_Cell()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#complete"))!.ParseHtmlTable();

                Assert.Multiple(() =>
                {

                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task Cell_By_Row_Index_And_Column_Name_Returns_The_Expected_Cell()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#complete"))!.ParseHtmlTable();

                Assert.Multiple(() =>
                {

                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task Cell_By_Text_Returns_The_Expected_Cell()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#complete"))!.ParseHtmlTable();

                Assert.Multiple(() =>
                {

                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task Cells_By_Text_Returns_The_Expected_Cells()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await (await Page.QuerySelectorAsync("#complete"))!.ParseHtmlTable();

                Assert.Multiple(() =>
                {

                });
            }

            else Assert.Fail("IPage Not Initialised");
        }
    }
}
