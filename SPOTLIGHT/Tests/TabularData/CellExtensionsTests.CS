namespace SPOTLIGHT.Tests.TabularData;

[TestFixture]
public class CellExtensionsTests
{
    [TestFixture]
    public class Content : TabularDataTestsBase
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
    public class InnerText : TabularDataTestsBase
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
    public class CellQuery : TabularDataTestsBase
    {
        // TODO: Add Tests For All Cell(s)ByIndex/Name Methods

        [Test]
        public async Task _01()
        {
            if (Page is not null)
            {
                Table<IElementHandle> table = await(await Page.QuerySelectorAsync("#complete"))!.ParseHtmlTable();

                Assert.Multiple(() =>
                {

                });
            }

            else Assert.Fail("IPage Not Initialised");
        }

        [Test]
        public async Task _02()
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
        public async Task _03()
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
        public async Task _04()
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
        public async Task _05()
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
        public async Task _06()
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
        public async Task _07()
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
        public async Task _08()
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
        public async Task _09()
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
        public async Task _10()
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
