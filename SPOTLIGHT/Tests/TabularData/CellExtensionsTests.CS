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
                Column<IElementHandle>? randomColumn = table.Columns.ColumnByIndex(randomColumnIndex);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(randomColumn?.Header, randomColumn?.HeaderBodyFooterCells().CellByRowIndex(0), $"Header Cell Of Column {randomColumnIndex} Is On Row Index 0");
                    Assert.AreEqual(randomColumn?.Cells.First(), randomColumn?.Cells.CellByRowIndex(1), $"First Row Cell Of Column {randomColumnIndex} Is On Row Index 1");
                    Assert.AreEqual(randomColumn?.Cells.Skip(1).Take(1).Single(), randomColumn?.Cells.CellByRowIndex(2), $"Second Row Cell Of Column {randomColumnIndex} Is On Row Index 2");
                    Assert.AreEqual(randomColumn?.Cells.Last(), randomColumn?.Cells.CellByRowIndex(3), $"Third Row Cell Of Column {randomColumnIndex} Is On Row Index 3");
                    Assert.AreEqual(randomColumn?.Footer, randomColumn?.HeaderBodyFooterCells().CellByRowIndex(4), $"Footer Cell Of Column {randomColumnIndex} Is On Row Index 4");
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
                    Assert.AreEqual(table.Header?.Cells, table.Cells().CellsByRowIndex(0), "Header Cells Are Identical To Cells In Row Index 0");
                    Assert.AreEqual(table.Rows.First().Cells, table.Cells().CellsByRowIndex(1), "First Row Cells Are Identical To Cells In Row Index 1");
                    Assert.AreEqual(table.Rows.Skip(1).Take(1).Single().Cells, table.Cells().CellsByRowIndex(2), "Second Row Cells Are Identical To Cells In Row Index 2");
                    Assert.AreEqual(table.Rows.Last().Cells, table.Cells().CellsByRowIndex(3), "Third Row Cells Are Identical To Cells In Row Index 3");
                    Assert.AreEqual(table.Footer?.Cells, table.Cells().CellsByRowIndex(4), "Footer Cells Are Identical To Cells In Row Index 4");
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
                int randomRowIndex = table.HeaderBodyFooterRows().RandomElement().Index;
                List<Cell<IElementHandle>>? randomRowCells = table.HeaderBodyFooterRows().RowByIndex(randomRowIndex)?.Cells;

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(randomRowCells?.First(), randomRowCells?.CellByColumnIndex(1), $"First Column Cell Of Row {randomRowIndex} Is On Column Index 1");
                    Assert.AreEqual(randomRowCells?.Skip(1).Take(1).Single(), randomRowCells?.CellByColumnIndex(2), $"Second Column Cell Of Row {randomRowIndex} Is On Column Index 2");
                    Assert.AreEqual(randomRowCells?.Last(), randomRowCells?.CellByColumnIndex(3), $"Third Column Cell Of Row {randomRowIndex} Is On Column Index 3");
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
                    Assert.AreEqual(table.Columns.First().HeaderBodyFooterCells(), table.Cells().CellsByColumnIndex(1), "First Column Cells Are Identical To Cells In Column Index 1");
                    Assert.AreEqual(table.Columns.Skip(1).Take(1).Single().HeaderBodyFooterCells(), table.Cells().CellsByColumnIndex(2), "Second Column Cells Are Identical To Cells In Column Index 2");
                    Assert.AreEqual(table.Columns.Last().HeaderBodyFooterCells(), table.Cells().CellsByColumnIndex(3), "Third Column Cells Are Identical To Cells In Column Index 3");
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
                int randomRowIndex = table.HeaderBodyFooterRows().RandomElement().Index;
                List<Cell<IElementHandle>>? randomRowCells = table.HeaderBodyFooterRows().RowByIndex(randomRowIndex)?.Cells;

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(randomRowCells?.First(), randomRowCells?.CellByColumnName("0-1"), @"First Column Cells Are Identical To Cells In Column With Name ""0-1""");
                    Assert.AreEqual(randomRowCells?.Skip(1).Take(1).Single(), randomRowCells?.CellByColumnName("0-2"), @"Second Column Cells Are Identical To Cells In Column With Name ""0-2""");
                    Assert.AreEqual(randomRowCells?.Last(), randomRowCells?.CellByColumnName("0-3"), @"Third Column Cells Are Identical To Cells In Column With Name ""0-3""");
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
                    Assert.AreEqual(table.Columns.First().HeaderBodyFooterCells(), table.Cells().CellsByColumnName("0-1"), @"First Column Cells Are Identical To Cells In Column With Name ""0-1""");
                    Assert.AreEqual(table.Columns.Skip(1).Take(1).Single().HeaderBodyFooterCells(), table.Cells().CellsByColumnName("0-2"), @"Second Column Cells Are Identical To Cells In Column With Name ""0-2""");
                    Assert.AreEqual(table.Columns.Last().HeaderBodyFooterCells(), table.Cells().CellsByColumnName("0-3"), @"Third Column Cells Are Identical To Cells In Column With Name ""0-3""");
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
                    Assert.AreEqual(table.Cells().CellsByRowIndex(0).Intersect(table.Cells().CellsByColumnIndex(1)).Single(), table.Cells().CellByIndex(0, 1), @"Cell With Index ""0-1"" Is At The Intersection Of Row Index 0 And Column Index 1");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(0).Intersect(table.Cells().CellsByColumnIndex(2)).Single(), table.Cells().CellByIndex(0, 2), @"Cell With Index ""0-2"" Is At The Intersection Of Row Index 0 And Column Index 2");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(0).Intersect(table.Cells().CellsByColumnIndex(3)).Single(), table.Cells().CellByIndex(0, 3), @"Cell With Index ""0-3"" Is At The Intersection Of Row Index 0 And Column Index 3");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(1).Intersect(table.Cells().CellsByColumnIndex(1)).Single(), table.Cells().CellByIndex(1, 1), @"Cell With Index ""1-1"" Is At The Intersection Of Row Index 1 And Column Index 1");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(1).Intersect(table.Cells().CellsByColumnIndex(2)).Single(), table.Cells().CellByIndex(1, 2), @"Cell With Index ""1-2"" Is At The Intersection Of Row Index 1 And Column Index 2");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(1).Intersect(table.Cells().CellsByColumnIndex(3)).Single(), table.Cells().CellByIndex(1, 3), @"Cell With Index ""1-3"" Is At The Intersection Of Row Index 1 And Column Index 3");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(2).Intersect(table.Cells().CellsByColumnIndex(1)).Single(), table.Cells().CellByIndex(2, 1), @"Cell With Index ""2-1"" Is At The Intersection Of Row Index 2 And Column Index 1");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(2).Intersect(table.Cells().CellsByColumnIndex(2)).Single(), table.Cells().CellByIndex(2, 2), @"Cell With Index ""2-2"" Is At The Intersection Of Row Index 2 And Column Index 2");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(2).Intersect(table.Cells().CellsByColumnIndex(3)).Single(), table.Cells().CellByIndex(2, 3), @"Cell With Index ""2-3"" Is At The Intersection Of Row Index 2 And Column Index 3");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(3).Intersect(table.Cells().CellsByColumnIndex(1)).Single(), table.Cells().CellByIndex(3, 1), @"Cell With Index ""3-1"" Is At The Intersection Of Row Index 3 And Column Index 1");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(3).Intersect(table.Cells().CellsByColumnIndex(2)).Single(), table.Cells().CellByIndex(3, 2), @"Cell With Index ""3-2"" Is At The Intersection Of Row Index 3 And Column Index 2");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(3).Intersect(table.Cells().CellsByColumnIndex(3)).Single(), table.Cells().CellByIndex(3, 3), @"Cell With Index ""3-3"" Is At The Intersection Of Row Index 3 And Column Index 3");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(4).Intersect(table.Cells().CellsByColumnIndex(1)).Single(), table.Cells().CellByIndex(4, 1), @"Cell With Index ""4-1"" Is At The Intersection Of Row Index 4 And Column Index 1");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(4).Intersect(table.Cells().CellsByColumnIndex(2)).Single(), table.Cells().CellByIndex(4, 2), @"Cell With Index ""4-2"" Is At The Intersection Of Row Index 4 And Column Index 2");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(4).Intersect(table.Cells().CellsByColumnIndex(3)).Single(), table.Cells().CellByIndex(4, 3), @"Cell With Index ""4-3"" Is At The Intersection Of Row Index 4 And Column Index 3");
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
                    Assert.AreEqual(table.Cells().CellsByRowIndex(0).Intersect(table.Cells().CellsByColumnName("0-1")).Single(), table.Cells().CellByRowIndexAndColumnName(0, "0-1"), @"Cell With Index ""0-1"" Is At The Intersection Of Row Index 0 And Column With Name ""0-1""");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(0).Intersect(table.Cells().CellsByColumnName("0-2")).Single(), table.Cells().CellByRowIndexAndColumnName(0, "0-2"), @"Cell With Index ""0-2"" Is At The Intersection Of Row Index 0 And Column With Name ""0-2""");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(0).Intersect(table.Cells().CellsByColumnName("0-3")).Single(), table.Cells().CellByRowIndexAndColumnName(0, "0-3"), @"Cell With Index ""0-3"" Is At The Intersection Of Row Index 0 And Column With Name ""0-3""");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(1).Intersect(table.Cells().CellsByColumnName("0-1")).Single(), table.Cells().CellByRowIndexAndColumnName(1, "0-1"), @"Cell With Index ""1-1"" Is At The Intersection Of Row Index 1 And Column With Name ""0-1""");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(1).Intersect(table.Cells().CellsByColumnName("0-2")).Single(), table.Cells().CellByRowIndexAndColumnName(1, "0-2"), @"Cell With Index ""1-2"" Is At The Intersection Of Row Index 1 And Column With Name ""0-2""");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(1).Intersect(table.Cells().CellsByColumnName("0-3")).Single(), table.Cells().CellByRowIndexAndColumnName(1, "0-3"), @"Cell With Index ""1-3"" Is At The Intersection Of Row Index 1 And Column With Name ""0-3""");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(2).Intersect(table.Cells().CellsByColumnName("0-1")).Single(), table.Cells().CellByRowIndexAndColumnName(2, "0-1"), @"Cell With Index ""2-1"" Is At The Intersection Of Row Index 2 And Column With Name ""0-1""");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(2).Intersect(table.Cells().CellsByColumnName("0-2")).Single(), table.Cells().CellByRowIndexAndColumnName(2, "0-2"), @"Cell With Index ""2-2"" Is At The Intersection Of Row Index 2 And Column With Name ""0-2""");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(2).Intersect(table.Cells().CellsByColumnName("0-3")).Single(), table.Cells().CellByRowIndexAndColumnName(2, "0-3"), @"Cell With Index ""2-3"" Is At The Intersection Of Row Index 2 And Column With Name ""0-3""");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(3).Intersect(table.Cells().CellsByColumnName("0-1")).Single(), table.Cells().CellByRowIndexAndColumnName(3, "0-1"), @"Cell With Index ""3-1"" Is At The Intersection Of Row Index 3 And Column With Name ""0-1""");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(3).Intersect(table.Cells().CellsByColumnName("0-2")).Single(), table.Cells().CellByRowIndexAndColumnName(3, "0-2"), @"Cell With Index ""3-2"" Is At The Intersection Of Row Index 3 And Column With Name ""0-2""");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(3).Intersect(table.Cells().CellsByColumnName("0-3")).Single(), table.Cells().CellByRowIndexAndColumnName(3, "0-3"), @"Cell With Index ""3-3"" Is At The Intersection Of Row Index 3 And Column With Name ""0-3""");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(4).Intersect(table.Cells().CellsByColumnName("0-1")).Single(), table.Cells().CellByRowIndexAndColumnName(4, "0-1"), @"Cell With Index ""4-1"" Is At The Intersection Of Row Index 4 And Column With Name ""0-1""");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(4).Intersect(table.Cells().CellsByColumnName("0-2")).Single(), table.Cells().CellByRowIndexAndColumnName(4, "0-2"), @"Cell With Index ""4-2"" Is At The Intersection Of Row Index 4 And Column With Name ""0-2""");
                    Assert.AreEqual(table.Cells().CellsByRowIndex(4).Intersect(table.Cells().CellsByColumnName("0-3")).Single(), table.Cells().CellByRowIndexAndColumnName(4, "0-3"), @"Cell With Index ""4-3"" Is At The Intersection Of Row Index 4 And Column With Name ""0-3""");
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
