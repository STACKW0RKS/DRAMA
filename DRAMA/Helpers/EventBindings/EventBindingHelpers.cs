namespace DRAMA.Helpers.EventBindings;

public static class EventBindingHelpers
{
    public static async Task<IPage> InitialiseBrowserTab(FeatureContext featureContext, BrowserNewContextOptions? browserNewContextOptions = null)
    {
        if (TestRunContext.Profile.TestRun?.DebugLogging is true)
            LogHelpers.Log($@"[DEBUG] [FEATURE___] INFO :: Current Playwright Version Is v{Assembly.GetAssembly(typeof(Playwright))?.GetName().Version}");

        Profile profile = TestRunContext.Profile;

        // TODO: Throw An Exception If Browser Driver Is NULL

        IPlaywright playwright = await Playwright.CreateAsync();

        // TODO: Make This Easier To Read

        IBrowser browser = profile.SystemUnderTest?.FrontEnd?.BrowserDriver?.Browser?.ToLower() switch
        {
            BrowserType.Chromium => await playwright.Chromium.LaunchAsync(profile.SystemUnderTest?.FrontEnd?.BrowserDriver?.BrowserOptions),
            BrowserType.Firefox => await playwright.Firefox.LaunchAsync(profile.SystemUnderTest?.FrontEnd?.BrowserDriver?.BrowserOptions),
            BrowserType.Webkit => await playwright.Webkit.LaunchAsync(profile.SystemUnderTest?.FrontEnd?.BrowserDriver?.BrowserOptions),
            _ => throw new NoMatchException($@"Invalid Browser Type ""{profile.SystemUnderTest?.FrontEnd?.BrowserDriver?.Browser}"" (Expected: chromium\firefox\webkit)")
        };

        browserNewContextOptions ??= new()
        {
            // TODO: Make These Configurable

            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 },
            IgnoreHTTPSErrors = true,
            RecordVideoDir = Path.Combine(profile.TestRun?.ResultsPath ?? string.Empty, featureContext.FeatureInfo.Title.ToPathCompatible()),
            RecordVideoSize = new RecordVideoSize() { Width = 1280, Height = 720 },
            RecordHarPath = Path.Combine(profile.TestRun?.ResultsPath ?? string.Empty, featureContext.FeatureInfo.Title.ToPathCompatible(), $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.har")
        };

        IBrowserContext browserContext = await browser.NewContextAsync(browserNewContextOptions);
        browserContext.SetDefaultTimeout(30000);

        await browserContext.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        featureContext.Set<IBrowser>("Browser", browser);

        IPage currentBrowserTab = await browserContext.NewPageAsync();

        featureContext.SetCurrentBrowserTab(currentBrowserTab);

        return currentBrowserTab;
    }

    public static async Task<IAPIRequestContext> InitialiseHTTPCallContext(FeatureContext featureContext, APIRequestNewContextOptions? apiRequestNewContextOptions = null)
    {
        Profile profile = TestRunContext.Profile;

        IPlaywright playwright = await Playwright.CreateAsync();

        if (apiRequestNewContextOptions is null)
        {
            apiRequestNewContextOptions = new()
            {
                // TODO: Set Some Configuration Options In The Profile, And Retrieve Them Here
                // e.g. API BaseURL Or Persistent HTTP Headers
            };

            if (profile.SystemUnderTest?.API?.AuthorisationScheme is not null && string.IsNullOrWhiteSpace(featureContext.GetAPIAuthenticationToken()).Equals(false))
                apiRequestNewContextOptions.ExtraHTTPHeaders = apiRequestNewContextOptions.ExtraHTTPHeaders is null
                    ? new List<KeyValuePair<string, string>>() { { new KeyValuePair<string, string>("Authorization", $"{profile.SystemUnderTest.API.AuthorisationScheme.ToTitleCase()} {featureContext.GetAPIAuthenticationToken()}") } }
                    : apiRequestNewContextOptions.ExtraHTTPHeaders.Append(new KeyValuePair<string, string>("Authorization", $"{profile.SystemUnderTest.API.AuthorisationScheme.ToTitleCase()} {featureContext.GetAPIAuthenticationToken()}"));
        }

        IAPIRequestContext currentHTTPCallContext = await playwright.APIRequest.NewContextAsync(apiRequestNewContextOptions);

        featureContext.SetCurrentHTTPCallContext(currentHTTPCallContext);

        return currentHTTPCallContext;
    }

    public static HttpClient InitialiseHTTPClient(FeatureContext featureContext)
    {
        HttpClient client = new(HTTPClientContext.HTTPHandler, disposeHandler: false);

        Profile profile = TestRunContext.Profile;

        if (profile.SystemUnderTest?.API?.AuthorisationScheme is not null && string.IsNullOrWhiteSpace(featureContext.GetAPIAuthenticationToken()).Equals(false))
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(profile.SystemUnderTest.API.AuthorisationScheme.ToTitleCase(), featureContext.GetAPIAuthenticationToken());

        featureContext.SetCurrentHTTPClient(client);

        return client;
    }
}
