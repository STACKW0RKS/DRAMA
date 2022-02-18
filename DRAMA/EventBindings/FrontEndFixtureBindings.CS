using DRAMA.Extensions.Specflow;

namespace DRAMA.EventBindings;

[Binding]
public class FrontEndFixtureBindings
{
    [BeforeFeature("Front-End", "End-To-End", Order = 0)]
    public static async Task BeforeFeature(TestThreadContext testThreadContext, FeatureContext featureContext)
    {
        Profile profile = testThreadContext.GetTestRunProfile();

        IPlaywright playwright = await Playwright.CreateAsync();

        IBrowser browser = profile.BrowserDriver?.Browser?.ToLower() switch
        {
            BrowserType.Chromium => await playwright.Chromium.LaunchAsync(profile.BrowserDriver?.BrowserOptions),
            BrowserType.Firefox => await playwright.Firefox.LaunchAsync(profile.BrowserDriver?.BrowserOptions),
            BrowserType.Webkit => await playwright.Webkit.LaunchAsync(profile.BrowserDriver?.BrowserOptions),
            _ => throw new NoMatchException($@"Invalid Browser Type ""{profile.BrowserDriver?.Browser}"" (Expected: Chromium\Firefox\WebKit)")
        };

        BrowserNewContextOptions browserNewContextOptions = new()
        {
            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 }, // TODO: Make Configurable
            IgnoreHTTPSErrors = true,
            RecordVideoDir = Path.Combine(profile.TestRun?.ResultsPath ?? string.Empty, featureContext.FeatureInfo.Title),
            RecordHarPath = Path.Combine(profile.TestRun?.ResultsPath ?? string.Empty, featureContext.FeatureInfo.Title, $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.har")
        };

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

    [AfterFeature("Front-End", "End-To-End", Order = 0)]
    public static async Task AfterFeature(TestThreadContext testThreadContext, FeatureContext featureContext)
        => await featureContext.GetProperty<IBrowser>("Browser").Contexts.Single().Tracing.StopAsync(new TracingStopOptions
            { Path = Path.Combine(testThreadContext.GetTestRunProfile().TestRun?.ResultsPath ?? string.Empty, featureContext.FeatureInfo.Title, $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.zip") });
}
