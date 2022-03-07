namespace DRAMA.EventBindings;

[Binding]
public sealed class FrontEndFixtureBindings
{
    [BeforeFeature("Front-End", "End-To-End", Order = 1)]
    public static async Task BeforeFeature(FeatureContext featureContext)
    {
        Profile profile = TestRunContext.Profile;

        // TODO: Throw An Exception If Browser Driver Is NULL

        IPlaywright playwright = await Playwright.CreateAsync();

        IBrowser browser = profile.BrowserDriver?.Browser?.ToLower() switch
        {
            BrowserType.Chromium => await playwright.Chromium.LaunchAsync(profile.BrowserDriver?.BrowserOptions),
            BrowserType.Firefox => await playwright.Firefox.LaunchAsync(profile.BrowserDriver?.BrowserOptions),
            BrowserType.Webkit => await playwright.Webkit.LaunchAsync(profile.BrowserDriver?.BrowserOptions),
            _ => throw new NoMatchException($@"Invalid Browser Type ""{profile.BrowserDriver?.Browser}"" (Expected: chromium\firefox\webkit)")
        };

        BrowserNewContextOptions browserNewContextOptions = new()
        {
            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 }, // TODO: Make Configurable
            IgnoreHTTPSErrors = true,
            RecordVideoDir = Path.Combine(profile.TestRun?.ResultsPath ?? string.Empty, featureContext.FeatureInfo.Title.ToPathCompatible()),
            RecordHarPath = Path.Combine(profile.TestRun?.ResultsPath ?? string.Empty, featureContext.FeatureInfo.Title.ToPathCompatible(), $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.har")
        }; // TODO: Find Out Why HAR Files Are Not Being Created Anymore (Maybe The Browser Type? Maybe The Path? Maybe Not Closing The Browser Context Properly?)

        IBrowserContext browserContext = await browser.NewContextAsync(browserNewContextOptions);
        browserContext.SetDefaultTimeout(30000);

        await browserContext.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        featureContext.SetProperty("Browser", browser);
        featureContext.SetCurrentBrowserTab(await browserContext.NewPageAsync());
    }

    [AfterFeature("Front-End", "End-To-End", Order = 1)]
    public static async Task AfterFeature(FeatureContext featureContext)
        => await featureContext.GetProperty<IBrowser>("Browser").Contexts.Single().Tracing.StopAsync(new TracingStopOptions
            { Path = Path.Combine(TestRunContext.Profile.TestRun?.ResultsPath ?? string.Empty, featureContext.FeatureInfo.Title.ToPathCompatible(), $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.zip") });
}
