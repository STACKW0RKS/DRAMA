namespace SPOTLIGHT.Tests.Base;

public abstract class TabularDataTests
{
    protected TabularDataTests()
        => InitialisePlaywrightDriver().Run();

    private IPlaywright? Driver { get; set; }
    private IBrowser? Browser { get; set; }
    protected IPage? Page { get; set; }
    private Uri? Playground { get; set; }

    ~TabularDataTests()
        => TerminatePlaywrightDriver().Run();

    private async Task InitialisePlaywrightDriver()
    {
        Driver = await Playwright.CreateAsync();
        Browser = await Driver.Firefox.LaunchAsync();
        Page = await Browser.NewPageAsync();
        Playground = new Uri(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "Resources", "Table-Parser-Unit-Testing-Playground.HTML"));

        await Page.GotoAsync(Playground.AbsoluteUri);
    }

    private async Task TerminatePlaywrightDriver()
    {
        if (Page is not null) await Page.CloseAsync();
        if (Browser is not null) await Browser.CloseAsync();

        Driver?.Dispose();
    }
}
